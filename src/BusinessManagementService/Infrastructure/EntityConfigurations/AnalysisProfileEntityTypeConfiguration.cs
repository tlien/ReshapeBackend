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
                .Property<Guid>("MediaTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("mediatypeid");

            builder
                .Property<Guid>("ScriptFileId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("scriptfileid");

            builder
                .Property<Guid>("ScriptParametersFileId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
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
                
            // builder
            //     .Property<Guid>("_mediaTypeId")
            //     .UsePropertyAccessMode(PropertyAccessMode.Field)
            //     .HasColumnName("mediatypeid");

            // builder
            //     .Property<Guid>("_scriptFileId")
            //     .UsePropertyAccessMode(PropertyAccessMode.Field)
            //     .HasColumnName("scriptfileid");

            // builder
            //     .Property<Guid>("_scriptParametersFileId")
            //     .UsePropertyAccessMode(PropertyAccessMode.Field)
            //     .HasColumnName("scriptparametersfileid");

            // builder
            //     .HasOne(a => a.MediaType)
            //     .WithMany()
            //     .HasForeignKey(a => a.GetMediaTypeId);

            // builder
            //     .HasOne(a => a.ScriptFile)
            //     .WithMany()
            //     .HasForeignKey(a => a.GetScriptFileId);

            // builder
            //     .HasOne(a => a.ScriptParametersFile)
            //     .WithMany()
            //     .HasForeignKey(a => a.GetScriptParametersFileId);
        }
    }
}