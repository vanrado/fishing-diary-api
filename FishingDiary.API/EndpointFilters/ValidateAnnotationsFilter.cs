using FishingDiaryAPI.Models;
using MiniValidation;

namespace FishingDiaryAPI.EndpointFilters
{
    public class ValidateAnnotationsFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var fisheryForCreationDto = context.GetArgument<FisheryForCreationDto>(2);
            if (!MiniValidator.TryValidate(fisheryForCreationDto, out var validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }  
            return await next(context);
        }
    }
}
