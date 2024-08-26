using Microsoft.AspNetCore.Mvc.Filters;

// we can handle exceptions,
// measure execution time,
// add parameter validation etc

namespace FilterDemo.Filters
{
  public class CustomActionFilter : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      // Log action method start
      Console.WriteLine($"Executing action method {context.ActionDescriptor.DisplayName}");

      // Optionally modify action parameters
      foreach (var param in context.ActionArguments)
      {
        Console.WriteLine($"Parameter {param.Key}: {param.Value}");
      }

      base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      // Log action method end
      Console.WriteLine($"Executed action method {context.ActionDescriptor.DisplayName}");

      if (context.Exception != null)
      {
        Console.WriteLine(context.Exception + "An error occurred while executing the action");
      }

      base.OnActionExecuted(context);
    }
  }
}
