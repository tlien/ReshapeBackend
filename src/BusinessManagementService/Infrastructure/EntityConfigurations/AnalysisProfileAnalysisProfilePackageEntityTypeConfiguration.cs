using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class AnalysisProfileAnalysisProfilePackageEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfileAnalysisProfilePackage>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfileAnalysisProfilePackage> builder)
        {
            builder.ToTable("analysisprofileanalysisprofilepackages");
            builder.HasKey(a => new { a.AnalysisProfileId, a.AnalysisProfilePackageId });

            builder
                .Property<Guid>("AnalysisProfileId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("analysisprofileid");

            builder
                .Property<Guid>("AnalysisProfilePackageId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("analysisprofilepackageid");

            // builder
            //     .HasOne(aap => aap.AnalysisProfilePackage)
            //     .WithMany(ap => ap.AnalysisProfileAnalysisProfilePackages)
            //     .HasForeignKey(aap => aap.AnalysisProfilePackageId);
            
            // builder
            //     .HasOne(aap => aap.AnalysisProfile)
            //     .WithMany(a => a.AnalysisProfileAnalysisProfilePackages)
            //     .HasForeignKey(aap => aap.AnalysisProfileId);
        }
    }
}