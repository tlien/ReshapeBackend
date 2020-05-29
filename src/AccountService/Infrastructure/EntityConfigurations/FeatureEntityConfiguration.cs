using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("features", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(f => f.Id);

            builder.Ignore(f => f.DomainEvents);

            // DDD: properties are private fields of the aggregate class
            // PropertyAccessMode.Field configures ef to correctly access these fields
            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired(true);

            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired(false);

            builder
                .HasMany<AccountFeature>()
                .WithOne(af => af.Feature)
                .HasForeignKey(af => af.FeatureId);
        }
    }
}