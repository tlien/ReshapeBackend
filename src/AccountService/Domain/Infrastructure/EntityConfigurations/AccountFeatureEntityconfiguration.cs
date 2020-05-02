using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.AggregatesModel.AccountAggregate;

namespace AccountService.Infrastructure {
    class AccountFeatureEntityConfiguration : IEntityTypeConfiguration<AccountFeature> {
        public void Configure(EntityTypeBuilder<AccountFeature> builder) {

            builder.HasKey(t => new { t.AccountId, t.FeatureId });
        }
    }

    public class AccountFeature {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid FeatureId { get; set; }
        private Feature Feature { get; set; }
    }
}