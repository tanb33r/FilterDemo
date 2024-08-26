using FilterDemo.Filters;
using FilterDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace FilterDemo.Controllers
{
  [CustomResultFilter]
  [ServiceFilter(typeof(CustomResultFilter))] // resource filter
  [ServiceFilter(typeof(CustomResourceFilter))] // resource filter
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    [CustomActionFilter] // action filter
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}