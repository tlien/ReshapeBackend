using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(EntityTypeBuilder<BusinessTier> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
        }
    }
}