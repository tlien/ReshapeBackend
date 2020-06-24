using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AccountFeatureEntityConfiguration : IEntityTypeConfiguration<AccountFeature>
    {
        public void Configure(EntityTypeBuilder<AccountFeature> builder)
        {
            builder.HasKey(t => new { t.AccountId, t.FeatureId });
        }
    }

    /// <summary>
    /// Represents a relation between an <c>Account</c> and a <c>Feature</c> used to map a many-to-many relation.
    /// </summary>
    public class AccountFeature
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}