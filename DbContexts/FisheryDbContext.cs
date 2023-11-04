using FishingDiaryAPI.Mocks;
using FishingDiaryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace FishingDiaryAPI.DbContexts
{
    public class FisheryDbContext : DbContext
    {
        public DbSet<Fishery> Fisheries { get; set; }
        public DbSet<UserFishery> UserFisheries { get; set; }

        public FisheryDbContext(DbContextOptions<FisheryDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the value converter for the List<Image> to persisted Json
            _ = modelBuilder.Entity<Fishery>()
                .Property(x => x.Images)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => (List<string>)c.ToList()))
                .HasColumnType("json");

            modelBuilder.Entity<UserFishery>()
                .HasKey(uf => new { uf.UserId, uf.FisheryId });
            modelBuilder.Entity<UserFishery>()
                .HasOne(uf => uf.Fishery)
                .WithMany()
                .HasForeignKey(uf => uf.FisheryId);


            // Seed Fishery table
            _ = modelBuilder.Entity<Fishery>().HasData(MockData.GetFisheryEntries());
            // Seed UserFishery table
            _ = modelBuilder.Entity<UserFishery>().HasData(
                new UserFishery { UserId = Guid.Parse("12345678-1234-5678-1234-567812345678"), FisheryId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), Id = Guid.NewGuid() }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
