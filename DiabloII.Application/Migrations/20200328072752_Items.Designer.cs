﻿// <auto-generated />

using System;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200328072752_Items")]
    partial class Items
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.ErrorLogs.Models.ErrorLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDateUtc");

                    b.HasKey("Id");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Items.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<double>("DexterityRequired");

                    b.Property<string>("ImageName");

                    b.Property<double>("Level");

                    b.Property<double>("LevelRequired");

                    b.Property<double>("MaximumDefenseMaximum");

                    b.Property<double>("MaximumDefenseMinimum");

                    b.Property<double>("MaximumOneHandedDamageMaximum");

                    b.Property<double>("MaximumOneHandedDamageMinimum");

                    b.Property<double>("MaximumTwoHandedDamageMaximum");

                    b.Property<double>("MaximumTwoHandedDamageMinimum");

                    b.Property<double>("MinimumDefenseMaximum");

                    b.Property<double>("MinimumDefenseMinimum");

                    b.Property<double>("MinimumOneHandedDamageMaximum");

                    b.Property<double>("MinimumOneHandedDamageMinimum");

                    b.Property<double>("MinimumTwoHandedDamageMaximum");

                    b.Property<double>("MinimumTwoHandedDamageMinimum");

                    b.Property<string>("Name");

                    b.Property<string>("Quality")
                        .IsRequired();

                    b.Property<double>("StrengthRequired");

                    b.Property<string>("SubCategory");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Items.Models.ItemProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstChararacter");

                    b.Property<string>("FormattedName");

                    b.Property<bool>("IsPercent");

                    b.Property<Guid>("ItemId");

                    b.Property<double>("Maximum");

                    b.Property<double>("Minimum");

                    b.Property<string>("Name");

                    b.Property<double>("OrderIndex");

                    b.Property<double>("Par");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ItemId");

                    b.ToTable("ItemProperties");
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Suggestions.Models.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Content")
                        .IsUnique();

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Suggestions.Models.SuggestionVote", b =>
                {
                    b.Property<Guid>("SuggestionId");

                    b.Property<string>("Ip")
                        .HasMaxLength(15);

                    b.Property<Guid>("Id");

                    b.Property<bool>("IsPositive");

                    b.Property<Guid?>("SuggestionId1");

                    b.HasKey("SuggestionId", "Ip");

                    b.HasAlternateKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SuggestionId1");

                    b.ToTable("SuggestionVotes");
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Items.Models.ItemProperty", b =>
                {
                    b.HasOne("DiabloII.Items.Api.DbContext.Items.Models.Item", "Item")
                        .WithMany("Properties")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiabloII.Items.Api.DbContext.Suggestions.Models.SuggestionVote", b =>
                {
                    b.HasOne("DiabloII.Items.Api.DbContext.Suggestions.Models.Suggestion")
                        .WithMany("Votes")
                        .HasForeignKey("SuggestionId1");
                });
#pragma warning restore 612, 618
        }
    }
}
