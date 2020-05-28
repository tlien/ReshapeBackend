using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations {
    public class FeatureEntityTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            // builder.ToTable("features");
            builder.HasKey(f => f.Id);
            builder.Ignore(f => f.DomainEvents);

            // builder
            //     .Property(f => f.Id)
            //     .HasColumnName("id");

            // builder
            //     .Property(f => f.Name)
            //     .HasColumnName("name");

            // builder
            //     .Property(f => f.Description)
            //     .HasColumnName("description");

            // builder
            //     .Property(f => f.Price)
            //     .HasColumnName("price");
        }
    }
}