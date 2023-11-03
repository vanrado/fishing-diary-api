using FishingDiaryAPI.Mocks;
using FishingDiaryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FishingDiaryAPI.EndpointHandlers
{
    public static class FishingDiaryHandlers
    {
        public static async Task<Ok<List<FishingDiaryEntryDto>>> GetFishingDiaryEntries()
        {
            // Mock database to store fishing diary entries
            var fishingDiaryEntries = MockData.GetMockFishingDiaryEntries();
            return TypedResults.Ok(fishingDiaryEntries);
        }

        public static async Task<Ok<FishingDiaryEntryDto>> CreateFishingDiaryEntry()
        {
            // TODO
            FishingDiaryEntryDto newEntry  = new();
            return TypedResults.Ok(newEntry);
        }

    }
}
