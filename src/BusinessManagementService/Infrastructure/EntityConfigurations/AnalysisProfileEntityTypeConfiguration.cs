using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations {
    public class AnalysisProfileEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {
        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.ToTable("AnalysisProfiles");
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvents);

            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired(false);

            builder
                .Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description")
                .IsRequired(false);

            builder
                .Property<string>("_fileName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("FileName")
                .IsRequired(false);

            builder
                .HasMany(ap => ap.RequiredFeatures)
                .WithOne(rf => rf.AnalysisProfile)
                .HasForeignKey(rf => rf.AnalysisProfileID);

            
            var navigation = builder.Metadata.FindNavigation(nameof(AnalysisProfile.RequiredFeatures));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}