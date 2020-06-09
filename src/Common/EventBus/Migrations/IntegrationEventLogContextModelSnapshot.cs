﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Reshape.Common.EventBus;

namespace Common.EventBus.Migrations
{
    [DbContext(typeof(IntegrationEventLogContext))]
    partial class IntegrationEventLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Reshape.Common.EventBus.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("event_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnName("event_type_name")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnName("state")
                        .HasColumnType("integer");

                    b.Property<int>("TimesSent")
                        .HasColumnName("times_sent")
                        .HasColumnType("integer");

                    b.Property<string>("TransactionId")
                        .HasColumnName("transaction_id")
                        .HasColumnType("text");

                    b.HasKey("EventId")
                        .HasName("pk_integration_event_logs");

                    b.ToTable("integration_event_logs");
                });
#pragma warning restore 612, 618
        }
    }
}
