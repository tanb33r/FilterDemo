using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
  public class CustomResultFilter : IResultFilter
  {
    private readonly ILogger<CustomResultFilter> _logger;
    private DateTime _startTime;

    public CustomResultFilter(ILogger<CustomResultFilter> logger)
    {
      _logger = logger;
    }

    //We can Modify Result
    //add customer response header etc

    public void OnResultExecuting(ResultExecutingContext context)
    {
      _startTime = DateTime.UtcNow;

      _logger.LogInformation("OnResultExecuting: Executing result of type {ResultType} for action {ActionName}",
          context.Result.GetType().Name, context.ActionDescriptor.DisplayName);

      // Modify the response header
      context.HttpContext.Response.Headers.Add("Custom-Header", "This is a custom header");

      // Optional: Modify the result (Example: Wrap the result in an additional object)
      if (context.Result is ObjectResult objectResult)
      {
        objectResult.Value = new
        {
          WrappedResponse = objectResult.Value,
          AdditionalInfo = "Some additional information"
        };
      }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      var duration = DateTime.UtcNow - _startTime;

      // Log the duration taken to execute the result
      _logger.LogInformation("OnResultExecuted: Completed result execution in {Duration} ms for action {ActionName}",
          duration.TotalMilliseconds, context.ActionDescriptor.DisplayName);

      // Check if an exception occurred and log it
      if (context.Exception != null)
      {
        _logger.LogError(context.Exception, "An error occurred during the result execution.");
        context.ExceptionHandled = true; // Optionally, mark the exception as handled
      }
    }
  }
}
