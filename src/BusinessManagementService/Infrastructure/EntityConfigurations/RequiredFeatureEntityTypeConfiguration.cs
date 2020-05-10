using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class RequiredFeatureEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfileRequiredFeature>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfileRequiredFeature> builder)
        {
            builder.ToTable("analysisprofilerequiredfeatures");
            builder.HasKey(rf => new { rf.AnalysisProfileID, rf.FeatureID });

            builder
                .Property<Guid>("AnalysisProfileID")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("analysisprofileid");

            builder
                .Property<Guid>("FeatureID")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("featureid"); 

            /* ## May be necessary if entityconfiguration of analysisprofile doesn't cut it ## */ 
            // builder
            //     .HasOne(rf => rf.AnalysisProfile)
            //     .WithMany(ap => ap.RequiredFeatures)
            //     .HasForeignKey(rf => rf.AnalysisProfileID);

            // builder
            //     .HasOne(rf => rf.Feature)
            //     .WithMany(f => f.AnalysisProfileRequiredFeatures)
            //     .HasForeignKey(rf => rf.FeatureID);
        }
    }
}