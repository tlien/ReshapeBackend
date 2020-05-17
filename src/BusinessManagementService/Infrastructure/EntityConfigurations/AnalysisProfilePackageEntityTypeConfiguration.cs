using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class AnalysisProfilePackageEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfilePackage>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfilePackage> builder)
        {
            builder.ToTable("analysisprofilepackages");
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvents);
            builder.Ignore(a => a.GetAnalysisProfiles);

            builder
                .Property<Guid>("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("id");

            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name");

            builder
                .Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description");

            builder
                .Property<decimal>("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("price");

            builder
                .HasMany(ap => ap.AnalysisProfileAnalysisProfilePackages)
                .WithOne(aap => aap.AnalysisProfilePackage)
                .HasForeignKey(ap => ap.AnalysisProfilePackageId);

            builder.Metadata
                .FindNavigation(nameof(AnalysisProfilePackage.AnalysisProfileAnalysisProfilePackages))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}