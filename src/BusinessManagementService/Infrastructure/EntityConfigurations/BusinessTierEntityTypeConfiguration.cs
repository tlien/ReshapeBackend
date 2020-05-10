using System;
using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class BusinessTierEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BusinessTier> builder)
        {
            builder.ToTable("businesstiers");
            builder.HasKey(b => b.Id);

            builder
                .Property<Guid>("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("id");

            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name");
        }
    }
}