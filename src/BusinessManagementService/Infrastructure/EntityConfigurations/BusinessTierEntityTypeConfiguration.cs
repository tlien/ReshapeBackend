using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations 
{
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BusinessTier> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
        }
    }
}