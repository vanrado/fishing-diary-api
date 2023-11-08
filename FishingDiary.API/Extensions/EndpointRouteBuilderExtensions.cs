using FishingDiary.API.EndpointFilters;
using FishingDiary.API.EndpointHandlers;
using FishingDiary.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FishingDiary.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterFisheriesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var fisheriesEndpoint = endpointRouteBuilder.MapGroup("/fisheries")
                .RequireAuthorization()
                .WithOpenApi();
            var fisheriesWithGuidIdEndpoints = fisheriesEndpoint.MapGroup("/{fisheryId:guid}");

            fisheriesEndpoint.MapGet("", FisheriesHandlers.GetFisheries)
                .WithSummary("Get a list of fisheries")
                .WithDescription("Retrieve a list of all fisheries available.");

            fisheriesEndpoint.MapGet("/search", FisheriesHandlers.SearchForFisheryByName)
                .WithSummary("Search for fisheries by name")
                .WithDescription("Search for fisheries based on their name.");

            fisheriesEndpoint.MapPost("", FisheriesHandlers.CreateFishery)
                .WithSummary("Create a new fishery")
                .WithDescription("Create a new fishery entry.")
                .AddEndpointFilter<ValidateAnnotationsFilter>()
                .ProducesValidationProblem();

            fisheriesEndpoint.MapGet("/favorites", FisheriesHandlers.GetFavoriteFisheries)
                .WithSummary("Get a user's favorite fisheries")
                .WithDescription("Retrieve a list of the user's favorite fisheries.")
                .WithName("GetFavoriteFisheries");

            fisheriesEndpoint.MapGet("/favorites/{userFisheryId:guid}", FisheriesHandlers.GetFavoriteFishery)
                .WithSummary("Get a favorite fishery")
                .WithDescription("This endpoint retrieves a favorite fishery for a user. The userFisheryId in the path should be replaced with the ID of the UserFishery resource.")
                .WithName("GetFavoriteFishery");

            fisheriesWithGuidIdEndpoints.MapGet("", FisheriesHandlers.GetFishery)
                .WithSummary("Get a fishery by ID")
                .WithDescription("Retrieve a specific fishery by its unique identifier.")
                .WithName("GetFishery");

            fisheriesWithGuidIdEndpoints.MapGet("/images", FisheriesHandlers.GetFisheryImages)
                .WithSummary("Get images of a fishery by ID")
                .WithDescription("Retrieve images associated with a specific fishery by its unique identifier.");

            fisheriesWithGuidIdEndpoints.MapPut("", FisheriesHandlers.UpdateFishery)
                .WithSummary("Update a fishery by ID")
                .WithDescription("Update the details of a specific fishery by its unique identifier.");

            fisheriesWithGuidIdEndpoints.MapDelete("", FisheriesHandlers.DeleteFishery)
                .WithSummary("Delete a fishery by ID")
                .WithDescription("Delete a specific fishery by its unique identifier.")
                .AddEndpointFilter(new FisheryIsLockedFilter(new("d28888e9-2ba9-473a-a40f-e38cb54f9b35")))
                .AddEndpointFilter<NotFoundResponseFilter>()
                .ProducesValidationProblem();

            fisheriesWithGuidIdEndpoints.MapPost("/favorite", FisheriesHandlers.AddFavoriteFishery)
                .WithSummary("Add a fishery to favorites")
                .WithDescription("Add a specific fishery to the user's list of favorite fisheries.");

            fisheriesWithGuidIdEndpoints.MapDelete("/favorite", FisheriesHandlers.DeleteFavoriteFishery)
                .WithSummary("Remove a fishery from favorites")
                .WithDescription("Remove a specific fishery from the user's list of favorite fisheries.");

        }

        public static void RegisterWeatherEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/weather/{latitude}/{longitude}", WeatherHandlers.GetLocationWeather)
                .RequireAuthorization()
                .Produces<WeatherDto>(200);
        }

        public static void RegisterFishingDiaryEntries(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/api/diary/entries", FishingDiaryHandlers.GetFishingDiaryEntries)
                .Produces<List<FishingDiaryEntryDto>>(200);

            endpointRouteBuilder.MapPost("/api/diary/entries", FishingDiaryHandlers.CreateFishingDiaryEntry)
                .Produces<FishingDiaryEntryDto>(201);
        }
    }
}
