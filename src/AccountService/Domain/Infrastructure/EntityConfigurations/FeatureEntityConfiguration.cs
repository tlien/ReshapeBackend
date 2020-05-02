using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.AggregatesModel.AccountAggregate;

namespace AccountService.Infrastructure {
    class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature> {
        public void Configure(EntityTypeBuilder<Feature> builder) {
            builder.ToTable("feature", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
        }
    }
}