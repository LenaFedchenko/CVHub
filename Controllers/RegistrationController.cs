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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
                return RedirectToAction("Success");
                
            }
            return View("Index");
        }
    }
}