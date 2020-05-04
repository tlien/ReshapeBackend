using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.AggregatesModel.AccountAggregate;

namespace AccountService.Infrastructure
{
    class BusinessTierEntityConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(EntityTypeBuilder<BusinessTier> builder)
        {
            builder.ToTable("businessTiers", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            // DDD: properties are private fields of the aggregate class
            // PropertyAccessMode.Field configures ef to correctly access these fields
            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired(true);
        }
    }
}