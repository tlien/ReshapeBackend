using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Ignore(f => f.DomainEvents);

            // one to many relation mapping, it represent one half of a many to many relation using two one-to-many relations
            builder
                .HasMany<AccountFeature>()
                .WithOne(af => af.Feature)
                .HasForeignKey(af => af.FeatureId);
        }
    }
}