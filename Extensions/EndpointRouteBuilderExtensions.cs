using FishingDiaryAPI.EndpointFilters;
using FishingDiaryAPI.EndpointHandlers;
using FishingDiaryAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FishingDiaryAPI.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterFisheriesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var fisheriesEndpoint = endpointRouteBuilder.MapGroup("/fisheries");
            var fisheriesWithGuidIdEndpoints = fisheriesEndpoint.MapGroup("/{fisheryId:guid}");

            fisheriesEndpoint.MapGet("", FisheriesHandlers.GetFisheries)
                .Produces<IEnumerable<FisheryDto>>(StatusCodes.Status200OK);
            fisheriesEndpoint.MapGet("/search", FisheriesHandlers.SearchForFisheryByName)
                .Produces<IEnumerable<FisheryDto>>(StatusCodes.Status200OK);
            fisheriesEndpoint.MapPost("", FisheriesHandlers.CreateFishery)
                .Produces<FisheryDto>(StatusCodes.Status201Created);
            fisheriesWithGuidIdEndpoints.MapGet("", FisheriesHandlers.GetFishery)
                .WithName("GetFishery")
                .Produces<FisheryDto>(StatusCodes.Status200OK)
                .Produces<NotFound>(StatusCodes.Status404NotFound);
            fisheriesWithGuidIdEndpoints.MapGet("/images", FisheriesHandlers.GetFisheryImages)
                .Produces<FisheryDto>(StatusCodes.Status200OK)
                .Produces<NotFound>(StatusCodes.Status404NotFound);
            fisheriesWithGuidIdEndpoints.MapPut("", FisheriesHandlers.UpdateFishery)
                .Produces<FisheryDto>(StatusCodes.Status200OK)
                .Produces<NotFound>(StatusCodes.Status404NotFound);
            fisheriesWithGuidIdEndpoints.MapDelete("", FisheriesHandlers.DeleteFishery)
                .AddEndpointFilter(new FisheryIsLockedFilter(new("d28888e9-2ba9-473a-a40f-e38cb54f9b35")))
                //.AddEndpointFilter(new FisheryIsLockedFilter(new("da2fd609-d754-4feb-8acd-c4f9ff13ba96")))
                .AddEndpointFilter<NotFoundResponseFilter>()
                .Produces<FisheryDto>(StatusCodes.Status204NoContent)
                .Produces<NotFound>(StatusCodes.Status404NotFound);
        }
    }
}
