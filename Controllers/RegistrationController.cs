using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CVHub.Data;

namespace CVHub.Controllers
{
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly AppDbContext _context;
        public RegistrationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";
            return View();
        }

        [HttpPost("Check")]
        public IActionResult Check(Registration data)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Registration
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Email = data.Email,
                    Password = data.Password
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();

                HttpContext.Session.SetString("IsEnter", "true");
                HttpContext.Session.SetString("UserName", newUser.Name);
                return RedirectToAction("Index", "Home");

            }
            return View("Index");
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            HttpContext.Session.SetString("IsEnter", "true");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}