using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Design;
using MediatR;

using Reshape.Common.SeedWork;
using Reshape.Common.DevelopmentTools;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    public class AccountContext : DbContext, IUnitOfWork, ISeeder<AccountContext>
    {
        public const string DEFAULT_SCHEMA = "account";
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusinessTier> BusinessTiers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<AnalysisProfile> AnalysisProfiles { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public AccountContext(DbContextOptions<AccountContext> opt) : base(opt) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public AccountContext(DbContextOptions<AccountContext> opt, IMediator mediator) : base(opt)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("AccountContext::ctor -> " + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusinessTierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountFeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountAnalysisProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // DO STUFF WITH DOMAIN EVENTS HERE!

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // DO STUFF WITH DOMAIN EVENTS HERE!

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

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

        // TODO: This seems like a WILD hack, maybe look into what implications it has.
        // Basically calling an extension method (static method on static object) INSIDE a method on the object it extends...
        // Pretty sure this is a code crime on some level.
        public AccountContext AddSeedData()
        {
            return AccountContextSeeder.AddSeedData(this);
        }
    }

    // Since Program has been heavily altered, meaning EF can't find the dbcontext during design time using that convention.
    // Providing a factory implementing IDesignTimeDbContextFactory solves this in a graceful manner
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseNpgsql(".", opt =>
            {
                opt.MigrationsAssembly(GetType().Assembly.GetName().Name);
                opt.EnableRetryOnFailure();
            })
            .UseSnakeCaseNamingConvention();

            return new AccountContext(optionsBuilder.Options);
        }
    }
}