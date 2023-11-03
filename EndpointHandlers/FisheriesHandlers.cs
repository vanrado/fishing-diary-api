using AutoMapper;
using FishingDiaryAPI.DbContexts;
using FishingDiaryAPI.Entities;
using FishingDiaryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishingDiaryAPI.EndpointHandlers
{
    public static class FisheriesHandlers
    {
        public static async Task<Ok<IEnumerable<FisheryDto>>> GetFisheries(FisheryDbContext fisheryDb, IMapper mapper, ILogger<FisheryDto> logger)
        {
            logger.LogInformation("GetFisheries...");
            {
                return TypedResults.Ok(mapper.Map<IEnumerable<FisheryDto>>(await fisheryDb.Fisheries.ToListAsync()));
            }
        }

        public static async Task<Results<NotFound, Ok<IEnumerable<FisheryDto>>>> SearchForFisheryByName(FisheryDbContext fisheryDb, IMapper mapper, [FromQuery] string fisheryName)
        {
            {
                var fisheryObject = await fisheryDb.Fisheries.Where(fishery => fishery.Title.Contains(fisheryName)).ToListAsync();
                var mappedDtos = mapper.Map<IEnumerable<FisheryDto>>(fisheryObject);
                return TypedResults.Ok(mappedDtos);
            }
        }

        public static async Task<CreatedAtRoute<FisheryDto>> CreateFishery(FisheryDbContext fisheryDb, IMapper mapper, FisheryForCreationDto fishery)
        {
            {
                var fisheryEntity = mapper.Map<Fishery>(fishery);
                fisheryDb.Add(fisheryEntity);
                await fisheryDb.SaveChangesAsync();
                var fisheryDto = mapper.Map<FisheryDto>(fisheryEntity);
                return TypedResults.CreatedAtRoute(
                    fisheryDto,
                    "GetFishery",
                    new
                    {
                        fisheryId = fisheryDto.Id
                    });
            }
        }

        public static async Task<Results<NotFound, Ok<FisheryDto>>> GetFishery(FisheryDbContext fisheryDb, Guid fisheryId, IMapper mapper)
        {
            {
                var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
                if (fisheryObject == null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(mapper.Map<FisheryDto>(fisheryObject));
            }
        }

        public static async Task<Results<NotFound, Ok<FisheryDto>>> UpdateFishery(FisheryDbContext fisheryDb, IMapper mapper, Guid fisheryId, FisheryForUpdate fisheryForUpdate)
        {
            {
                var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
                if (fisheryObject == null)
                {
                    return TypedResults.NotFound();
                }
                mapper.Map(fisheryForUpdate, fisheryObject);
                await fisheryDb.SaveChangesAsync();
                return TypedResults.Ok(mapper.Map<FisheryDto>(fisheryObject));
            }
        }

        public static async Task<Results<NotFound, NoContent>> DeleteFishery(FisheryDbContext fisheryDb, Guid fisheryId)
        {
            {
                var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
                if (fisheryObject == null)
                {
                    return TypedResults.NotFound();
                }
                fisheryDb.Remove(fisheryObject);
                await fisheryDb.SaveChangesAsync();
                return TypedResults.NoContent();
            }
        }

        public static async Task<Results<NotFound, Ok<List<string>>>> GetFisheryImages(FisheryDbContext fisheryDb, Guid fisheryId)
        {
            {
                var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
                if (fisheryObject != null)
                {
                    return TypedResults.Ok(fisheryObject.Images);
                }
                else
                {
                    return TypedResults.NotFound();
                }
            }
        }
    }
}
