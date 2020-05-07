using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class RequiredFeatureEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfileRequiredFeature>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfileRequiredFeature> builder)
        {
            builder.ToTable("AnalysisProfileRequiredFeatures");
            builder.HasKey(rf => new { rf.AnalysisProfileID, rf.FeatureID });

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