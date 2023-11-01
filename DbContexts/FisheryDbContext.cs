using FishingDiaryAPI.Mocks;
using FishingDiaryAPI.Models;
using Microsoft.EntityFrameworkCore;

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

            _ = modelBuilder.Entity<User>().HasData(new User { Id = Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c") });

            _ = modelBuilder.Entity<User>().HasMany(user => user.fisheries).WithMany().UsingEntity(mnTable => mnTable.HasData(new { UserId = Guid.Parse("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"), fisheriesId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96") }));


            base.OnModelCreating(modelBuilder);
        }
    }
}
