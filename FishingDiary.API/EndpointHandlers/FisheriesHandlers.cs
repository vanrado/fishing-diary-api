using AutoMapper;
using FishingDiary.API.DbContexts;
using FishingDiary.API.Entities;
using FishingDiary.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishingDiary.API.EndpointHandlers
{
    public static class FisheriesHandlers
    {
        public static async Task<Ok<IEnumerable<FisheryDto>>> GetFisheries(FisheryDbContext fisheryDb, IMapper mapper, ILogger<FisheryDto> logger)
        {

            return TypedResults.Ok(mapper.Map<IEnumerable<FisheryDto>>(await fisheryDb.Fisheries.ToListAsync()));
        }

        public static async Task<Results<NotFound, Ok<IEnumerable<FisheryDto>>>> SearchForFisheryByName(FisheryDbContext fisheryDb, IMapper mapper, [FromQuery] string fisheryName)
        {

            var fisheryObject = await fisheryDb.Fisheries.Where(fishery => fishery.Title.Contains(fisheryName)).ToListAsync();
            var mappedDtos = mapper.Map<IEnumerable<FisheryDto>>(fisheryObject);
            return TypedResults.Ok(mappedDtos);

        }

        public static async Task<CreatedAtRoute<FisheryDto>> CreateFishery(FisheryDbContext fisheryDb, IMapper mapper, FisheryForCreationDto fishery)
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

        public static async Task<Results<NotFound, Ok<FisheryDto>>> GetFishery(FisheryDbContext fisheryDb, Guid fisheryId, IMapper mapper)
        {

            var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
            if (fisheryObject == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<FisheryDto>(fisheryObject));
        }

        public static async Task<Results<NotFound, Ok<FisheryDto>>> UpdateFishery(FisheryDbContext fisheryDb, IMapper mapper, Guid fisheryId, FisheryForUpdate fisheryForUpdate)
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

        public static async Task<Results<NotFound, NoContent>> DeleteFishery(FisheryDbContext fisheryDb, Guid fisheryId)
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

        public static async Task<Results<NotFound, Ok<List<string>>>> GetFisheryImages(FisheryDbContext fisheryDb, Guid fisheryId)
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

        public static async Task<Ok<IEnumerable<FisheryDto>>> GetFavoriteFisheries(FisheryDbContext fisheryDb, IMapper mapper, HttpContext context)
        {
            var userId = Guid.Parse(context.User.GetUserId());

            var fisheries = await fisheryDb.UserFisheries
                .Where(fishery => fishery.UserId == userId)
                .Select(fishery => fishery.Fishery)
                .ToListAsync();
            var fisheryDtos = mapper.Map<IEnumerable<FisheryDto>>(fisheries);
            return TypedResults.Ok(fisheryDtos);
        }

        public static async Task<Results<NotFound, Ok<UserFisheryDto>>> GetFavoriteFishery(FisheryDbContext fisheryDb, IMapper mapper, Guid userFisheryId)
        {
            var userFishery = await fisheryDb.UserFisheries
                .FirstOrDefaultAsync(userFishery => userFishery.Id == userFisheryId);

            if (userFishery == null)
            {
                return TypedResults.NotFound();
            }

            var userFisheryDto = mapper.Map<UserFisheryDto>(userFishery);
            return TypedResults.Ok(userFisheryDto);
        }

        // Delete the specified fishery from the user's favorites in the database
        public static async Task<Results<NotFound, NoContent>> DeleteFavoriteFishery(HttpContext context, FisheryDbContext fisheryDb, Guid fisheryId)
        {

            var userId = Guid.Parse(context.User.GetUserId());

            // check if the user fishery association exists
            var userFishery = await fisheryDb.UserFisheries.FirstOrDefaultAsync(userFishery => userFishery.UserId == userId && userFishery.FisheryId == fisheryId);
            if (userFishery == null)
            {
                return TypedResults.NotFound();
            }


            // remove the user fishery association
            fisheryDb.UserFisheries.Remove(userFishery);
            await fisheryDb.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        public static async Task<Results<NotFound, Conflict, CreatedAtRoute<UserFisheryDto>>> AddFavoriteFishery(FisheryDbContext fisheryDb, HttpContext context, Guid fisheryId, IMapper mapper)
        {
            var userId = Guid.Parse(context.User.GetUserId());

            // check if the fishery exists
            var fishery = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
            if (fishery == null)
            {
                return TypedResults.NotFound();
            }

            // check if the user has already favorited the fishery
            var userFishery = await fisheryDb.UserFisheries.FirstOrDefaultAsync(userFishery => userFishery.UserId == userId && userFishery.FisheryId == fisheryId);
            if (userFishery != null)
            {
                return TypedResults.Conflict();
            }

            // Add a new entry to the UserFishery
            var userFisheryEntity = new UserFishery
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FisheryId = fisheryId
            };
            fisheryDb.UserFisheries.Add(userFisheryEntity);
            await fisheryDb.SaveChangesAsync();

            // return result
            var userFisheryDto = mapper.Map<UserFisheryDto>(userFisheryEntity);
            return TypedResults.CreatedAtRoute(userFisheryDto, "GetFavoriteFishery", new { userFisheryId = userFisheryEntity.Id });
        }
    }
}
