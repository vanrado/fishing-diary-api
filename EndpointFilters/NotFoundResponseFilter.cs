﻿using System.Net;

namespace FishingDiaryAPI.EndpointFilters
{
    public class NotFoundResponseFilter : IEndpointFilter
    {
        private readonly ILogger<NotFoundResponseFilter> _logger;

        public NotFoundResponseFilter(ILogger<NotFoundResponseFilter> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Console.WriteLine($"NotFoundResponseFilter");
            var result = await next(context);
            var httpResult = (result is INestedHttpResult result1) ? result1.Result : (IResult)result;

            if ((httpResult as IStatusCodeHttpResult).StatusCode == (int)HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"Resource {context.HttpContext.Request.Path} was not found.");
            }

            return result;
        }
    }
}