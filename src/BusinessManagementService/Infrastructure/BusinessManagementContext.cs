using System.Threading;
using System.Threading.Tasks;
using Reshape.Common.SeedWork;
using Microsoft.EntityFrameworkCore;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Reshape.BusinessManagementService.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Design;

namespace Reshape.BusinessManagementService.Infrastructure {

    public class BusinessManagementContext : DbContext, IUnitOfWork {

        public DbSet<AnalysisProfile> AnalysisProfiles { get; set; }
        public DbSet<BusinessTier> BusinessTiers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<ScriptFile> ScriptFiles { get; set; }
        public DbSet<ScriptParametersFile> ScriptParametersFiles { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public BusinessManagementContext (DbContextOptions<BusinessManagementContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public BusinessManagementContext (DbContextOptions<BusinessManagementContext> options, IMediator mediator) : base(options) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessTierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MediaTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriptParametersFileEntityTypeConfiguration());
        }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

    }

    // Allow webhosting extension to migrate database at design time
    // The factory is accessed simply by being in the same project root or namespace Reshape.as the context it is producing, hence no code references to the factory
    public class BusinessManagementContextFactory : IDesignTimeDbContextFactory<BusinessManagementContext>
    {
        public BusinessManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessManagementContext>();

            optionsBuilder.UseNpgsql(".", options => options.MigrationsAssembly(GetType().Assembly.GetName().Name)).UseSnakeCaseNamingConvention();

            return new BusinessManagementContext(optionsBuilder.Options);
        }
    }

}