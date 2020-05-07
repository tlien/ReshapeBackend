using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class FeatureEntityTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");
            builder.HasKey(f => f.Id);

            builder
                .Property("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name");

            builder
                .Property("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description");

            builder
                .HasMany<AnalysisProfileRequiredFeature>()
                .WithOne(rf => rf.Feature)
                .HasForeignKey(rf => rf.FeatureID);
        }
    }
}