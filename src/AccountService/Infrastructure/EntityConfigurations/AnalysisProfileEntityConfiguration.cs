using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AnalysisProfileEntityConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.ToTable("analysisProfiles", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(ap => ap.Id);

            builder.Ignore(ap => ap.DomainEvents);

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
                .HasMany<AccountAnalysisProfile>()
                .WithOne(aap => aap.AnalysisProfile)
                .HasForeignKey(aap => aap.AnalysisProfileId);
        }
    }
}