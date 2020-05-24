using System.Threading;
using System.Threading.Tasks;
using Common.SeedWork;
using Microsoft.EntityFrameworkCore;
using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using BusinessManagementService.Infrastructure.EntityConfigurations;

namespace BusinessManagementService.Infrastructure {

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

}