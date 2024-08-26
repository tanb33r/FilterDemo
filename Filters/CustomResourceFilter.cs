using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters;

public class CustomResourceFilter : IResourceFilter
{
  private readonly ILogger<CustomResourceFilter> _logger;
  private readonly IMemoryCache _cache;

  public CustomResourceFilter(ILogger<CustomResourceFilter> logger, IMemoryCache cache)
  {
    _logger = logger;
    _cache = cache;
  }

  public void OnResourceExecuting(ResourceExecutingContext context)
  {
    // Log request details
    _logger.LogInformation($"Executing Resource: {context.HttpContext.Request.Path}");

    // Check if the request is cached
    var cacheKey = context.HttpContext.Request.Path.ToString();
    if (GetFromCache(cacheKey, out IActionResult cachedResponse))
    {
      Console.WriteLine("Loading from cache: OnResourceExecuting");
      context.Result = cachedResponse; // Short-circuit the pipeline if cached response is found
    }
  }

  public void OnResourceExecuted(ResourceExecutedContext context)
  {
    // Cache the response if necessary
    var cacheKey = context.HttpContext.Request.Path.ToString();
    CacheResponse(cacheKey, context.Result);
  }

  private bool GetFromCache(string cacheKey, out IActionResult cachedResponse)
  {
    return _cache.TryGetValue(cacheKey, out cachedResponse);
  }

  private void CacheResponse(string cacheKey, IActionResult response)
  {
    // Set cache options
    var cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache for 5 minutes

    // Save data in cache
    _cache.Set(cacheKey, response, cacheEntryOptions);
  }
}