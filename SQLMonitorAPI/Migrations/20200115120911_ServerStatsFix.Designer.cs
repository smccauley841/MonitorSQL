﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLMonitorAPI.Data;

namespace SQLMonitorAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200115120911_ServerStatsFix")]
    partial class ServerStatsFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SQLMonitorAPI.Models.BackupHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BackupTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("BackupType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Database")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstanceID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InstanceID");

                    b.ToTable("BackupHistory");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.DatabaseFileSize", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DatabaseID")
                        .HasColumnType("int");

                    b.Property<int?>("InstanceID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalDataSizeBytes")
                        .HasColumnType("int");

                    b.Property<int>("TotalDataUsedBytes")
                        .HasColumnType("int");

                    b.Property<int>("TotalLogSizeBytes")
                        .HasColumnType("int");

                    b.Property<int>("TotalLogUsedBytes")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DatabaseID");

                    b.HasIndex("InstanceID");

                    b.ToTable("DatabaseFileSize");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.SQLDatabases", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Collation")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("InstanceID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("RecoveryModel")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("InstanceID");

                    b.ToTable("SQLDatabases");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.SQLInstance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IsAlwaysOn")
                        .HasColumnType("int");

                    b.Property<int>("IsClustered")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NoOfDatabases")
                        .HasColumnType("int");

                    b.Property<string>("OSName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalServerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SQLCollation")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("SQLCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SQLEdition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SQLVersion")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("SQLInstances");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.ServerStats", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhysicalServerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ServerStat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("ServerStats");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.BackupHistory", b =>
                {
                    b.HasOne("SQLMonitorAPI.Models.SQLInstance", "Instance")
                        .WithMany("BackupHistory")
                        .HasForeignKey("InstanceID");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.DatabaseFileSize", b =>
                {
                    b.HasOne("SQLMonitorAPI.Models.SQLDatabases", "Database")
                        .WithMany("DatabaseFileSize")
                        .HasForeignKey("DatabaseID");

                    b.HasOne("SQLMonitorAPI.Models.SQLInstance", "Instance")
                        .WithMany("DatabaseFileSize")
                        .HasForeignKey("InstanceID");
                });

            modelBuilder.Entity("SQLMonitorAPI.Models.SQLDatabases", b =>
                {
                    b.HasOne("SQLMonitorAPI.Models.SQLInstance", "Instance")
                        .WithMany("SQLDatabases")
                        .HasForeignKey("InstanceID");
                });
#pragma warning restore 612, 618
        }
    }
}
