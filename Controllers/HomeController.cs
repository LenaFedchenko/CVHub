using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CVHub.Models;

namespace CVHub.Controllers;

public class HomeController : Controller
{
    public static bool IsEnter = false;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
