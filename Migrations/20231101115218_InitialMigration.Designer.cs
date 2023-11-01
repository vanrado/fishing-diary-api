﻿// <auto-generated />
using System;
using FishingDiaryAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FishingDiaryAPI.Migrations
{
    [DbContext(typeof(FisheryDbContext))]
    [Migration("20231101115218_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("FisheryUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("fisheriesId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "fisheriesId");

                    b.HasIndex("fisheriesId");

                    b.ToTable("FisheryUser");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"),
                            fisheriesId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96")
                        });
                });

            modelBuilder.Entity("FishingDiaryAPI.Models.Fishery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Fisheries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                            Title = "VN Evička"
                        },
                        new
                        {
                            Id = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                            Title = "OR Melečka č. 1"
                        });
                });

            modelBuilder.Entity("FishingDiaryAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ebe94d5d-2ad8-4886-b246-05a1fad83d1c")
                        });
                });

            modelBuilder.Entity("FisheryUser", b =>
                {
                    b.HasOne("FishingDiaryAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FishingDiaryAPI.Models.Fishery", null)
                        .WithMany()
                        .HasForeignKey("fisheriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
