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
                .Property(s => s.Id)
                .HasColumnName("id");

            builder
                .Property(s => s.Name)
                .HasColumnName("name");
            
            builder
                .Property(s => s.Description)
                .HasColumnName("description");

            builder
                .Property(s => s.Script)
                .HasColumnName("script");
        }
    }
}