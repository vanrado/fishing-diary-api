using FishingDiary.API.Mocks;
using FishingDiary.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FishingDiary.API.EndpointHandlers
{
    public static class FishingDiaryHandlers
    {
        public static Ok<List<FishingDiaryEntryDto>> GetFishingDiaryEntries()
        {
            // Mock database to store fishing diary entries
            var fishingDiaryEntries = MockData.GetMockFishingDiaryEntries();
            return TypedResults.Ok(fishingDiaryEntries);
        }

        public static Ok<FishingDiaryEntryDto> CreateFishingDiaryEntry()
        {
            // TODO
            FishingDiaryEntryDto newEntry = new();
            return TypedResults.Ok(newEntry);
        }

    }
}
