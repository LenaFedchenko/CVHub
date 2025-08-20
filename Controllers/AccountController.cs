using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CVHub.Data;

namespace CVHub.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    public AccountController(AppDbContext context)
    {
        _context = context;
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

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Account data)
    {
        if (!ModelState.IsValid)
            return View("Index", data);

        if (data.PhotoFile != null && data.PhotoFile.Length > 0)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(data.PhotoFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/photo", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await data.PhotoFile.CopyToAsync(stream);
            }

            data.Photo = "/images/photo/" + fileName;
        }


        // склеиваем чекбоксы в строку
        data.Skills = data.SelectedSkills != null ? string.Join(",", data.SelectedSkills) : "";

        await _context.Accounts.AddAsync(data);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetString("IsEnter", "true");
        HttpContext.Session.SetString("UserName", data.Name);

        return RedirectToAction("Index", "Account");
    }


    [HttpGet("Login")]
    public IActionResult Login()
    {
        HttpContext.Session.SetString("IsEnter", "true");
        return RedirectToAction("Index", "Account");
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("Index");
    }


}