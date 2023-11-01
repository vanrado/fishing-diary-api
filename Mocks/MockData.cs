using FishingDiaryAPI.Models;

namespace FishingDiaryAPI.Mocks
{
    public static class MockData
    {
        public static List<FishingDiaryEntryDto> GetMockFishingDiaryEntries()
        {
            return new List<FishingDiaryEntryDto>
        {
            new FishingDiaryEntryDto
            {
                FishSpecies = "Bass",
                DateTimeOfCatch = DateTime.UtcNow.AddDays(-5),
                FishingLocation = "Lake A"
            },
            new FishingDiaryEntryDto
            {
                FishSpecies = "Trout",
                DateTimeOfCatch = DateTime.UtcNow.AddDays(-10),
                FishingLocation = "River B"
            },
            new FishingDiaryEntryDto
            {
                FishSpecies = "Catfish",
                DateTimeOfCatch = DateTime.UtcNow.AddDays(-15),
                FishingLocation = "Pond C"
            },
            new FishingDiaryEntryDto
            {
                FishSpecies = "Salmon",
                DateTimeOfCatch = DateTime.UtcNow.AddDays(-20),
                FishingLocation = "River D"
            },
            new FishingDiaryEntryDto
            {
                FishSpecies = "Perch",
                DateTimeOfCatch = DateTime.UtcNow.AddDays(-25),
                FishingLocation = "Lake E"
            }
        };
        }

        public static List<Fishery> GetFisheryEntries()
        {
            return new List<Fishery>
        {
            new Fishery
            {
                Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Title = "VN Evička",
                Images =
                {
                    "https://www.fishsurfing.com/cdn/fspw-sk-images/30769/5d426de9.webp",
                    "https://www.fishsurfing.com/cdn/fspw-sk-images/30769/7a0e641c.webp",
                }
            },
            new Fishery
            {
                Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                Title = "OR Melečka č. 1",
                Images =
                {
                    "https://www.fishsurfing.com/cdn/fspw-sk-images/31274/09ce1094.webp",
                    "https://www.fishsurfing.com/cdn/fspw-sk-images/31274/2731d261.webp",
                }
            },
        };
        }
    }
}
