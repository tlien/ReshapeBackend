using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.AggregatesModel.AccountAggregate;

namespace AccountService.Infrastructure
{
    class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(o => o.DomainEvents);

            // DDD: Value Object
            builder.OwnsOne(o => o.address, a => a.WithOwner());
            builder.OwnsOne(o => o.contactDetails, c => c.WithOwner());

            builder.Property<BusinessTier>("_businessTier")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("businessTier");

            var navigation = builder.Metadata.FindNavigation(nameof(Account.features));

            // DDD Patterns comment:
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany<AccountFeature>()
                .WithOne(af => af.Account);
        }
    }
}