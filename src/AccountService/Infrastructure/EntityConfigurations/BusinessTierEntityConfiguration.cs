using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure
{
    class BusinessTierEntityConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(EntityTypeBuilder<BusinessTier> builder)
        {
            builder.ToTable("businesstiers", AccountContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);
        }
    }
}