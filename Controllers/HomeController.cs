using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CVHub.Models;
using CVHub.Data;
using Microsoft.EntityFrameworkCore;

namespace CVHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";
        
        var accounts = await _context.Accounts
            .Include(a => a.Registration)
            .Where(a => a.Registration != null)
            .ToListAsync();

        return View(accounts);
    }


    [HttpGet("resume/{id}")]
    public async Task<IActionResult> ViewResume(int id)
    {
        var account = await _context.Accounts
            .Include(a => a.Registration)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(account.Skills))
            account.SelectedSkills = account.Skills.Split(',');

        ViewBag.IsReadOnly = true; // Флаг для режима только чтения
        ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";
        
        return View("~/Views/Account/Index.cshtml", account);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}