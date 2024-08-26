namespace FilterDemo.Controllers
{
  public class CustomResultFilterAttribute : Attribute
  {
    public void OnResultExecuting(Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext context)
    {
      Console.WriteLine("OnResultExecuting Attribute");
      // Pre-processing logic before the result
    }

    public void OnResultExecuted(Microsoft.AspNetCore.Mvc.Filters.ResultExecutedContext context)
    {
      Console.WriteLine("OnResultExecuted Attribute");
      // Post-processing logic after the result
    }
  }
}