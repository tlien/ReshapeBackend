using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessManagementService.Infrastructure.EntityConfigurations
{
    public class MediaTypeEntityTypeConfiguration : IEntityTypeConfiguration<MediaType>
    {
        public void Configure(EntityTypeBuilder<MediaType> builder)
        {
            builder.ToTable("mediatypes");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Name).IsUnique();
            builder.Ignore(m => m.DomainEvents);

            builder
                .Property(m => m.Id)
                .HasColumnName("id");

            builder
                .Property(m => m.Name)
                .HasColumnName("name");
        }
    }
}