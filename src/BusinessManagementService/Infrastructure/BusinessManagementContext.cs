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
        public DbSet<AnalysisProfileRequiredFeature> AnalysisProfileRequiredFeatures { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public BusinessManagementContext (DbContextOptions<BusinessManagementContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public BusinessManagementContext (DbContextOptions<BusinessManagementContext> options, IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AnalysisProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessTierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RequiredFeatureEntityTypeConfiguration());
        }
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

    }

}