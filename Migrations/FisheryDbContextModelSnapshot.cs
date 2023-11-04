﻿// <auto-generated />
using System;
using FishingDiaryAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FishingDiaryAPI.Migrations
{
    [DbContext(typeof(FisheryDbContext))]
    partial class FisheryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("FishingDiaryAPI.Entities.Fishery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Fisheries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                            Images = "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/5d426de9.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/7a0e641c.webp\"]",
                            Title = "VN Evička"
                        },
                        new
                        {
                            Id = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                            Images = "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/09ce1094.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/2731d261.webp\"]",
                            Title = "OR Melečka č. 1"
                        });
                });

            modelBuilder.Entity("FishingDiaryAPI.Entities.UserFishery", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FisheryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "FisheryId");

                    b.HasIndex("FisheryId");

                    b.ToTable("UserFisheries");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("12345678-1234-5678-1234-567812345678"),
                            FisheryId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                            Id = new Guid("39de575e-61a0-4b8c-8762-6c9bcd43f3ec")
                        });
                });

            modelBuilder.Entity("FishingDiaryAPI.Entities.UserFishery", b =>
                {
                    b.HasOne("FishingDiaryAPI.Entities.Fishery", "Fishery")
                        .WithMany()
                        .HasForeignKey("FisheryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fishery");
                });
#pragma warning restore 612, 618
        }
    }
}
