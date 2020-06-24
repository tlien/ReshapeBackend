using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvents);
            builder.Ignore(x => x.Features);
            builder.Ignore(x => x.AnalysisProfiles);

            // DDD: Value Objects persisted as owned entity type (same table, separate object)
            builder.OwnsOne(a => a.Address, ad => ad.WithOwner());
            builder.OwnsOne(a => a.ContactDetails, c => c.WithOwner());


            // Navigation mappings
            builder.Metadata
                .FindNavigation(nameof(Account.BusinessTier))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            var featuresNav = builder.Metadata.FindNavigation(nameof(Account.AccountFeatures));
            featuresNav.SetPropertyAccessMode(PropertyAccessMode.Field);
            featuresNav.SetField("_accountFeatures");

            var analysisprofilesNav = builder.Metadata.FindNavigation(nameof(Account.AccountAnalysisProfiles));
            analysisprofilesNav.SetPropertyAccessMode(PropertyAccessMode.Field);
            analysisprofilesNav.SetField("_accountAnalysisProfiles");
        }
    }
}