using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations 
{
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BusinessTier> builder)
        {
            // builder.ToTable("businesstiers");
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);

            // builder
            //     .Property(b => b.Id)
            //     .HasColumnName("id");

            // builder
            //     .Property(b => b.Name)
            //     .HasColumnName("name");

            // builder
            //     .Property(b => b.Description)
            //     .HasColumnName("description");

            // builder
            //     .Property(b => b.Price)
            //     .HasColumnName("price");
        }
    }
}