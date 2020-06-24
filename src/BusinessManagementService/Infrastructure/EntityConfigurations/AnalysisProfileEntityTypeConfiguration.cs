using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class AnalysisProfileEntityTypeConfiguration : IEntityTypeConfiguration<AnalysisProfile>
    {

        public void Configure(EntityTypeBuilder<AnalysisProfile> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvents);

            builder.Metadata
                .FindNavigation(nameof(AnalysisProfile.MediaType));

            builder.Metadata
                .FindNavigation(nameof(AnalysisProfile.ScriptFile));

            builder.Metadata
                .FindNavigation(nameof(AnalysisProfile.ScriptParametersFile));
        }
    }
}