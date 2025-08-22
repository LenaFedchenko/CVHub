using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVHub.Data;
using CVHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CVHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
    
            return View();
        }

        [HttpPost]
        public IActionResult Login(int userId)
        {

            HttpContext.Session.SetInt32("UserId", userId);
            HttpContext.Session.SetString("IsEnter", "true");

            return RedirectToAction("Index", "Account");
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var account = _context.Accounts.FirstOrDefault(a => a.RegistrationId == userId.Value);

            if (account == null)
                return View(new Account());

            if (!string.IsNullOrEmpty(account.Skills))
                account.SelectedSkills = account.Skills.Split(',');

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account data)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.RegistrationId == userId.Value);

            if (existingAccount != null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
                return View("Index", data);

            if (data.PhotoFile != null && data.PhotoFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(data.PhotoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/photo", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await data.PhotoFile.CopyToAsync(stream);

                data.Photo = "/images/photo/" + fileName;
            }

            data.Skills = data.SelectedSkills != null ? string.Join(",", data.SelectedSkills) : "";
            data.RegistrationId = userId.Value;

            await _context.Accounts.AddAsync(data);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UserName", data.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Account data)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View("Index", data);

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.RegistrationId == userId.Value);

            if (account == null)
                return NotFound();


            account.Name = data.Name;
            account.Surname = data.Surname;
            account.Email = data.Email;
            account.Linkedin = data.Linkedin;
            account.Github = data.Github;
            account.Age = data.Age;
            account.Role = data.Role;
            account.Seniority = data.Seniority;
            account.PlaceEarly = data.PlaceEarly;
            account.Experience = data.Experience;
            account.Skills = data.SelectedSkills != null ? string.Join(",", data.SelectedSkills) : "";

            if (data.PhotoFile != null && data.PhotoFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(data.PhotoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/photo", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await data.PhotoFile.CopyToAsync(stream);

                account.Photo = "/images/photo/" + fileName;
            }

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UserName", account.Name);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}