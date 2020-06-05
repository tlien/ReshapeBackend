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

            builder
                .HasMany<AccountAnalysisProfile>()
                .WithOne(aap => aap.AnalysisProfile)
                .HasForeignKey(aap => aap.AnalysisProfileId);
        }
    }
}