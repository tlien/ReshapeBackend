﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Reshape.BusinessManagementService.Infrastructure;

namespace BusinessManagementService.Infrastructure.Migrations
{
    [DbContext(typeof(BusinessManagementContext))]
    [Migration("20200609131815_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.AnalysisProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<Guid>("MediaTypeId")
                        .HasColumnName("media_type_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ScriptFileId")
                        .HasColumnName("script_file_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScriptParametersFileId")
                        .HasColumnName("script_parameters_file_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_analysis_profiles");

                    b.HasIndex("MediaTypeId")
                        .HasName("ix_analysis_profiles_media_type_id");

                    b.HasIndex("ScriptFileId")
                        .HasName("ix_analysis_profiles_script_file_id");

                    b.HasIndex("ScriptParametersFileId")
                        .HasName("ix_analysis_profiles_script_parameters_file_id");

                    b.ToTable("analysis_profiles");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.MediaType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_media_types");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("ix_media_types_name");

                    b.ToTable("media_types");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.ScriptFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Script")
                        .HasColumnName("script")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_script_files");

                    b.ToTable("script_files");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.ScriptParametersFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("ScriptParameters")
                        .HasColumnName("script_parameters")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_script_parameters_files");

                    b.ToTable("script_parameters_files");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate.BusinessTier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric");

                    b.HasKey("Id")
                        .HasName("pk_business_tiers");

                    b.ToTable("business_tiers");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric");

                    b.HasKey("Id")
                        .HasName("pk_features");

                    b.ToTable("features");
                });

            modelBuilder.Entity("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.AnalysisProfile", b =>
                {
                    b.HasOne("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.MediaType", "MediaType")
                        .WithMany()
                        .HasForeignKey("MediaTypeId")
                        .HasConstraintName("fk_analysis_profiles_media_types_media_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.ScriptFile", "ScriptFile")
                        .WithMany()
                        .HasForeignKey("ScriptFileId")
                        .HasConstraintName("fk_analysis_profiles_script_files_script_file_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate.ScriptParametersFile", "ScriptParametersFile")
                        .WithMany()
                        .HasForeignKey("ScriptParametersFileId")
                        .HasConstraintName("fk_analysis_profiles_script_parameters_files_script_parameters")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}