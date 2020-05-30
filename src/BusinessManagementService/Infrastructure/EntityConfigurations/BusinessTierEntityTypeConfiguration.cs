using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations 
{
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(EntityTypeBuilder<BusinessTier> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
        }
    }
}