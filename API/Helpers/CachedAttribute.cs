using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace API.Helpers
{
    // Custom attribute to use cached if possible or create cache if not found
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        // Time for cached items to live 
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        // Attribute method that runs before and after controller runs
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Obtain the cache service, generate a cache key using the query params in the request, and check if cache exists for that key
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            // If cached result exists, send result from cache
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }

            // If no cached result exists, execute controller and create a cache entry using the result from the controller
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        // Order the query params and create a key string for redis to use
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
