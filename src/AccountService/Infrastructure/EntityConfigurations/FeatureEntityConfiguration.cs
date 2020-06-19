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

            builder
                .HasMany<AccountFeature>()
                .WithOne(af => af.Feature)
                .HasForeignKey(af => af.FeatureId);
        }
    }
}