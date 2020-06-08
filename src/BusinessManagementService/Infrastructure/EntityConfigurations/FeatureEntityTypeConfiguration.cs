using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class FeatureEntityTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Ignore(f => f.DomainEvents);
        }
    }
}