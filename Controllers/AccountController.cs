using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CVHub.Data;
using Microsoft.EntityFrameworkCore;

namespace CVHub.Controllers;

public class AccountController : Controller
{

    
    private readonly AppDbContext _context;
    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? id)
{
    ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";

    Account? account;

    if (id.HasValue)
    {
        account = _context.Accounts.FirstOrDefault(a => a.Id == id);
    }
    else
    {
        account = _context.Accounts.OrderByDescending(a => a.Id).FirstOrDefault();
    }

    if (account == null)
        return View(new Account());

    if (!string.IsNullOrEmpty(account.Skills))
        account.SelectedSkills = account.Skills.Split(',');

    return View(account);
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

        return RedirectToAction("Index", "Account", new { id = data.Id });
    }

    [HttpPost("Update")]
    
public async Task<IActionResult> Update(Account data)
    {
        if (!ModelState.IsValid)
            return View("Index", data);

        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == data.Id);
        if (account == null)
            return NotFound();

        // обновляем поля
        account.Name = data.Name;
        account.Surname = data.Surname;
        account.Email = data.Email;
        account.Linkedin = data.Linkedin;
        account.Github = data.Github;
        account.Age = data.Age;
        account.Role = data.Role;
        account.Seniority = data.Seniority;
        account.PlaceEarly = data.PlaceEarly;
        account.Expirience = data.Expirience;
        account.Skills = data.SelectedSkills != null ? string.Join(",", data.SelectedSkills) : "";

        if (data.PhotoFile != null && data.PhotoFile.Length > 0)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(data.PhotoFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/photo", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await data.PhotoFile.CopyToAsync(stream);
            }

            account.Photo = "/images/photo/" + fileName;
        }

        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetString("IsEnter", "true");
        HttpContext.Session.SetString("UserName", account.Name);

        return RedirectToAction("Index", "Account", new { id = account.Id });
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