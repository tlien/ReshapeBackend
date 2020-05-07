using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BusinessTier> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name");
        }
    }
}