﻿// <auto-generated />
using System;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DiabloII.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiabloII.Domain.Models.ErrorLogs.ErrorLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DexterityRequired")
                        .HasColumnType("float");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Level")
                        .HasColumnType("float");

                    b.Property<double>("LevelRequired")
                        .HasColumnType("float");

                    b.Property<double>("MaximumDefenseMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MaximumDefenseMinimum")
                        .HasColumnType("float");

                    b.Property<double>("MaximumOneHandedDamageMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MaximumOneHandedDamageMinimum")
                        .HasColumnType("float");

                    b.Property<double>("MaximumTwoHandedDamageMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MaximumTwoHandedDamageMinimum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumDefenseMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumDefenseMinimum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumOneHandedDamageMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumOneHandedDamageMinimum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumTwoHandedDamageMaximum")
                        .HasColumnType("float");

                    b.Property<double>("MinimumTwoHandedDamageMinimum")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StrengthRequired")
                        .HasColumnType("float");

                    b.Property<string>("SubCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Items.ItemProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstChararacter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormattedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPercent")
                        .HasColumnType("bit");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Maximum")
                        .HasColumnType("float");

                    b.Property<double>("Minimum")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OrderIndex")
                        .HasColumnType("float");

                    b.Property<double>("Par")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ItemId");

                    b.ToTable("ItemProperties");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Suggestions.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Content")
                        .IsUnique();

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Suggestions.SuggestionComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SuggestionId");

                    b.ToTable("SuggestionComments");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Suggestions.SuggestionVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPositive")
                        .HasColumnType("bit");

                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SuggestionId");

                    b.ToTable("SuggestionVotes");
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Items.ItemProperty", b =>
                {
                    b.HasOne("DiabloII.Domain.Models.Items.Item", "Item")
                        .WithMany("Properties")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Suggestions.SuggestionComment", b =>
                {
                    b.HasOne("DiabloII.Domain.Models.Suggestions.Suggestion", null)
                        .WithMany("Comments")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiabloII.Domain.Models.Suggestions.SuggestionVote", b =>
                {
                    b.HasOne("DiabloII.Domain.Models.Suggestions.Suggestion", null)
                        .WithMany("Votes")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
