﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Reshape.AccountService.Infrastructure;

namespace AccountService.API.Infrastructure.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20200605225958_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BusinessTierId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BusinessTierId");

                    b.ToTable("accounts","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.AnalysisProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("analysisProfiles","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.BusinessTier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("businesstiers","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("features","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Infrastructure.AccountAnalysisProfile", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnalysisProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("AccountId", "AnalysisProfileId");

                    b.HasIndex("AnalysisProfileId");

                    b.ToTable("accountanalysisProfile","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Infrastructure.AccountFeature", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uuid");

                    b.HasKey("AccountId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("accountfeatures","account");
                });

            modelBuilder.Entity("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Account", b =>
                {
                    b.HasOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.BusinessTier", "BusinessTier")
                        .WithMany()
                        .HasForeignKey("BusinessTierId");

                    b.OwnsOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<string>("Street1")
                                .HasColumnType("text");

                            b1.Property<string>("Street2")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("text");

                            b1.HasKey("AccountId");

                            b1.ToTable("accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.OwnsOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uuid");

                            b1.Property<string>("ContactPersonFullName")
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .HasColumnType("text");

                            b1.Property<string>("Phone")
                                .HasColumnType("text");

                            b1.HasKey("AccountId");

                            b1.ToTable("accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });
                });

            modelBuilder.Entity("Reshape.AccountService.Infrastructure.AccountAnalysisProfile", b =>
                {
                    b.HasOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Account", "Account")
                        .WithMany("AccountAnalysisProfiles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.AnalysisProfile", "AnalysisProfile")
                        .WithMany()
                        .HasForeignKey("AnalysisProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Reshape.AccountService.Infrastructure.AccountFeature", b =>
                {
                    b.HasOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Account", "Account")
                        .WithMany("AccountFeatures")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reshape.AccountService.Domain.AggregatesModel.AccountAggregate.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
