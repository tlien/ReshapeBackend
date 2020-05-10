using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvents);

            // DDD: Value Objects persisted as owned entity type
            builder.OwnsOne(a => a.address, ad => ad.WithOwner());
            builder.OwnsOne(a => a.contactDetails, c => c.WithOwner());

            // DDD: properties are private fields of the aggregate class
            // PropertyAccessMode.Field configures ef to correctly access these fields
            builder.Property<bool>("_isActive")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("isActive")
                .IsRequired(true);

            builder.Property<BusinessTier>("_businessTier")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("businessTier")
                .IsRequired(true);

            // Features collection
            var navigation = builder.Metadata.FindNavigation(nameof(Account.features));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany<AccountFeature>()
                .WithOne(af => af.Account);
        }
    }
}