using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CVHub.Controllers
{
    [Route("[controller]")]
    public class AuthorisationController : Controller
    {
        private readonly ILogger<AuthorisationController> _logger;

        public AuthorisationController(ILogger<AuthorisationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpPost]
        // public IActionResult CheckUser(Authorisation data)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var logUser = new Authorisation
        //         {
        //             Email = data.Email,
        //             Password = data.Password
        //         };

        //         return RedirectToAction("Success");
                
        //     }
        //     return View("Index");
        // }

    }
}