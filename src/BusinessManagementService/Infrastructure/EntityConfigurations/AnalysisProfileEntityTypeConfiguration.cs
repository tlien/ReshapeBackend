using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations 
{
    public class AnalysisProfileEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {

        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.ToTable("analysisprofiles");
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvents);

            builder
                .Property<Guid>(a => a.Id)
                .HasColumnName("id");

            builder
                .Property(a => a.Name)
                .HasColumnName("name");

            builder
                .Property(a => a.Description)
                .HasColumnName("description");

            builder
                .Property(a => a.Price)
                .HasColumnName("price");

            builder
                .Property(a => a.MediaTypeId)
                .HasColumnName("mediatypeid");

            builder
                .Property(a => a.ScriptFileId)
                .HasColumnName("scriptfileid");

            builder
                .Property(a => a.ScriptParametersFileId)
                .HasColumnName("scriptparametersfileid");

            builder
                .HasOne(a => a.MediaType)
                .WithMany()
                .HasForeignKey(a => a.MediaTypeId);

            builder
                .HasOne(a => a.ScriptFile)
                .WithMany()
                .HasForeignKey(a => a.ScriptFileId);

            builder
                .HasOne(a => a.ScriptParametersFile)
                .WithMany()
                .HasForeignKey(a => a.ScriptParametersFileId);
        }
    }
}