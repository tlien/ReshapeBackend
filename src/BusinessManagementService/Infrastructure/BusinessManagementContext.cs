using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
// using MediatR;

using Reshape.Common.DevelopmentTools;
// using Reshape.Common.Extensions;
using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Infrastructure.EntityConfigurations;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure
{
    /// <summary>
    /// <c>DbContext</c> used in the <c>BusinessManagement</c> microservice.
    /// Setup to reflect the business management domain.
    /// Extends the <c>DbContext</c> class and implements <c>IUnitOfWork</c>.
    /// </summary>
    public class BusinessManagementContext : DbContext, IUnitOfWork, ISeeder<BusinessManagementContext>
    {
        public DbSet<AnalysisProfile> AnalysisProfiles { get; set; }
        public DbSet<BusinessTier> BusinessTiers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<ScriptFile> ScriptFiles { get; set; }
        public DbSet<ScriptParametersFile> ScriptParametersFiles { get; set; }

        // private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public BusinessManagementContext(DbContextOptions<BusinessManagementContext> options) : base(options) { }

        /// <summary>
        /// Gets the current transaction if one exists.
        /// </summary>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// Gets whether or not the context holds an active transaction.
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        // public BusinessManagementContext(DbContextOptions<BusinessManagementContext> options, IMediator mediator) : base(options)
        // {
        //     _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessTierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MediaTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptParametersFileEntityTypeConfiguration());
        }

        /// <summary>
        /// Save changes made to all tracked entities to the database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // DO STUFF WITH DOMAIN EVENTS HERE!
            // await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Begins a new database transaction unless a transaction is already tracked in the <c>BusinessManagementContext</c>.
        /// The transaction has <c>IsolationLevel.ReadCommitted</c>, allowing outside transactions to read (but not write to)
        /// the volatile data (data affected during the transaction).
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
        /// if the transaction to commit is currently tracked by the <c>BusinessManagementContext</c>.
        /// Should any errors occur, the transaction is rolled back.
        /// See <c>BusinessManagementContext.RollbackTransaction()</c> for more info.
        /// </summary>
        /// <param name="transaction">The transaction to commit</param>
        /// <returns></returns>
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
        public BusinessManagementContext AddSeedData()
        {
            return BusinessManagementContextSeeder.AddSeedData(this);
        }
    }
}