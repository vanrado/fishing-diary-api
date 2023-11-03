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
                .Produces<FisheryDto>(StatusCodes.Status204NoContent)
                .Produces<NotFound>(StatusCodes.Status404NotFound);
        }
    }
}
