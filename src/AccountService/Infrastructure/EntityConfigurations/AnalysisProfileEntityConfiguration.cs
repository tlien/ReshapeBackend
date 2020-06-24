using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AnalysisProfileEntityConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.HasKey(ap => ap.Id);

            builder.Ignore(ap => ap.DomainEvents);

            // one to many relation mapping, it represent one half of a many to many relation using two one-to-many relations
            builder
                .HasMany<AccountAnalysisProfile>()
                .WithOne(aap => aap.AnalysisProfile)
                .HasForeignKey(aap => aap.AnalysisProfileId);
        }
    }
}