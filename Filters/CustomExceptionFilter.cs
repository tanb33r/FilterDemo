namespace FilterDemo.Filters
{
  public class CustomExceptionFilter : Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter
  {
    public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
    {
      // Handle exception
      // Example: Logging, custom error response
      Console.WriteLine("OnException");
    }
  }
}
