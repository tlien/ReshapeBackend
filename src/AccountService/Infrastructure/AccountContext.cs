using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
// using MediatR;

using Reshape.Common.DevelopmentTools;
// using Reshape.Common.Extensions;
using Reshape.Common.SeedWork;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    /// <summary>
    /// <c>DbContext</c> used in the <c>Account</c> microservice.
    /// Setup to reflect the account domain.
    /// Extends the <c>DbContext</c> class and implements <c>IUnitOfWork</c>.
    /// </summary>
    public class AccountContext : DbContext, IUnitOfWork, ISeeder<AccountContext>
    {
        public const string DEFAULT_SCHEMA = "account";
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusinessTier> BusinessTiers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<AnalysisProfile> AnalysisProfiles { get; set; }

        // private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public AccountContext(DbContextOptions<AccountContext> opt) : base(opt) { }

        /// <summary>
        /// Gets the current transaction if one exists.
        /// </summary>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// Gets whether or not the context holds an active transaction.
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        // public AccountContext(DbContextOptions<AccountContext> opt, IMediator mediator) : base(opt)
        // {
        //     _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusinessTierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountFeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountAnalysisProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        }

        /// <summary>
        /// Save changes made to all tracked entities to the database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // DO STUFF WITH DOMAIN EVENTS HERE!
            // await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Begins a new database transaction unless a transaction is already tracked in the <c>AccountContext</c>.
        /// The transaction has <c>IsolationLevel.ReadCommitted</c>, this ensures reads are always the latest in-memory version
        /// during an active transaction.
        /// </summary>
        /// <returns>A <c>Task</c> that returns the transaction once awaited.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        /// <summary>
        /// Saves changes to the database and commits the transaction,
        /// if the transaction to commit is currently tracked by the <c>AccountContext</c>.
        /// Should any errors occur, the transaction is rolled back.
        /// See <c>AccountContext.RollbackTransaction()</c> for more info.
        /// </summary>
        /// <param name="transaction">The transaction to commit</param>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// Rolls back the current transaction, effectively discarding and reversing
        /// all changes made to volatile data during the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        // TODO: This seems like a dumb hack, maybe look into what implications it has.
        // Basically calling an extension method (static method on static object) INSIDE a method on the object it extends...
        // Pretty sure this is a code crime on some level.
        public AccountContext AddSeedData()
        {
            return AccountContextSeeder.AddSeedData(this);
        }
    }
}