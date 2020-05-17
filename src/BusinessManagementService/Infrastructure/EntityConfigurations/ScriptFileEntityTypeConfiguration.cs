using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class ScriptFileEntityTypeConfiguration : IEntityTypeConfiguration<ScriptFile>
    {
        public void Configure(EntityTypeBuilder<ScriptFile> builder)
        {
            builder.ToTable("scriptfiles");
            builder.HasKey(s => s.Id);
            builder.Ignore(s => s.DomainEvents);

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
                .Property<string>("_script")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("script");
        }
    }
}