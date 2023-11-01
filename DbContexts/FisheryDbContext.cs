using FishingDiaryAPI.Mocks;
using FishingDiaryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using Newtonsoft.Json;
using System.Text.Json;

namespace FishingDiaryAPI.DbContexts
{
    public class FisheryDbContext : DbContext
    {
        public DbSet<Fishery> Fisheries { get; set; }

        public FisheryDbContext(DbContextOptions<FisheryDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Fishery>().HasData(MockData.GetFisheryEntries());
            // Configure the value converter for the List<Image> to persisted Json
            _ = modelBuilder.Entity<Fishery>()
                .Property(x => x.Images)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions) null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions) null),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => (List<string>) c.ToList()))
                .HasColumnType("json");


            _ = modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c") });

            _ = modelBuilder.Entity<User>().HasMany(user => user.fisheries).WithMany().UsingEntity(mnTable => mnTable.HasData(new { UserId = Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"), fisheriesId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96") }));


            base.OnModelCreating(modelBuilder);
        }
    }
}
