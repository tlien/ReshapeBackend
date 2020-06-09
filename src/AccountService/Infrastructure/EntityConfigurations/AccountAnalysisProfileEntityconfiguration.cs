using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AccountAnalysisProfileEntityConfiguration : IEntityTypeConfiguration<AccountAnalysisProfile>
    {
        public void Configure(EntityTypeBuilder<AccountAnalysisProfile> builder)
        {
            builder.HasKey(t => new { t.AccountId, t.AnalysisProfileId });
        }
    }

    public class AccountAnalysisProfile
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid AnalysisProfileId { get; set; }
        public AnalysisProfile AnalysisProfile { get; set; }
    }
}