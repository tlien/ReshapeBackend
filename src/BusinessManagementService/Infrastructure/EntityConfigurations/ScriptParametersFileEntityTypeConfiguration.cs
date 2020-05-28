using System;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class ScriptParametersFileEntityTypeConfiguration : IEntityTypeConfiguration<ScriptParametersFile>
    {
        public void Configure(EntityTypeBuilder<ScriptParametersFile> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Ignore(s => s.DomainEvents);
        }
    }
}