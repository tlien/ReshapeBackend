using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class FeatureEntityTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("features");
            builder.HasKey(f => f.Id);
            builder.Ignore(f => f.DomainEvents);

            builder
                .Property<Guid>("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("id");

            builder
                .Property("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name");

            builder
                .Property("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description");

            builder
                .Property("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("price");
        }
    }
}