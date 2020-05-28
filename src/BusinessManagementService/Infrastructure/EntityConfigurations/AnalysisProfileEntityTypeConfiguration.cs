using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations 
{
    public class AnalysisProfileEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {

        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvents);

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