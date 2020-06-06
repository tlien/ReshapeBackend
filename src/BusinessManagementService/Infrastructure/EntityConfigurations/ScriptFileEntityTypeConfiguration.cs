using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class ScriptFileEntityTypeConfiguration : IEntityTypeConfiguration<ScriptFile>
    {
        public void Configure(EntityTypeBuilder<ScriptFile> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Ignore(s => s.DomainEvents);
        }
    }
}