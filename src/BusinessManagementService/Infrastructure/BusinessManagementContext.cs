using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using MediatR;

using Reshape.Common.DevelopmentTools;
using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Infrastructure.EntityConfigurations;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure
{
    public class BusinessManagementContext : DbContext, IUnitOfWork, ISeeder<BusinessManagementContext>
    {

        public DbSet<AnalysisProfile> AnalysisProfiles { get; set; }
        public DbSet<BusinessTier> BusinessTiers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<ScriptFile> ScriptFiles { get; set; }
        public DbSet<ScriptParametersFile> ScriptParametersFiles { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public BusinessManagementContext(DbContextOptions<BusinessManagementContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public BusinessManagementContext(DbContextOptions<BusinessManagementContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessTierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MediaTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptParametersFileEntityTypeConfiguration());
        }

        // public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        // {
        //     await _mediator.DispatchDomainEventsAsync(this);
        //     var result = await base.SaveChangesAsync(cancellationToken);
        //     return true;
        // }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
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
        public BusinessManagementContext AddSeedData()
        {
            return BusinessManagementContextSeeder.AddSeedData(this);
        }
    }

    // Allow webhosting extension to migrate database at design time
    // The factory is accessed simply by being in the same project root or namespace Reshape.as the context it is producing, hence no code references to the factory
    public class BusinessManagementContextFactory : IDesignTimeDbContextFactory<BusinessManagementContext>
    {
        public BusinessManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessManagementContext>();

            optionsBuilder.UseNpgsql(".", options =>
                {
                    options.MigrationsAssembly(GetType().Assembly.GetName().Name);
                    options.EnableRetryOnFailure();
                }
            ).UseSnakeCaseNamingConvention();

            return new BusinessManagementContext(optionsBuilder.Options);
        }
    }

}