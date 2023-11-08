namespace FishingDiaryAPI.EndpointFilters
{
    public class FisheryIsLockedFilter : IEndpointFilter
    {

        private readonly Guid _lockedFisheryId;

        public FisheryIsLockedFilter(Guid lockedFisheryId)
        {
            _lockedFisheryId = lockedFisheryId;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var fisheryId = context.GetArgument<Guid>(1);
            if (fisheryId == _lockedFisheryId)
            {
                return TypedResults.Problem(new()
                {
                    Status = 400,
                    Title = "Fishery is perfect and cannot be changed.",
                    Detail = "You cannot update or delete perfection."
                });
            }
            var result = await next(context);
            return result;
        }
    }
}
