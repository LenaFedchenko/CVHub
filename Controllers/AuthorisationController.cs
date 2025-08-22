using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CVHub.Data;
using Microsoft.AspNetCore.Http;

namespace CVHub.Controllers
{
    [Route("[controller]")]
    public class AuthorisationController : Controller
    {
        private readonly ILogger<AuthorisationController> _logger;
        private readonly AppDbContext _context;

        public AuthorisationController(AppDbContext dbContext, ILogger<AuthorisationController> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsEnter") == "true")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("CheckUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckUser(Authorisation data)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", data);
            }

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == data.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "User not found or incorrect password");
                    return View("Index", data);
                }

                if (user.Password != data.Password)
                {
                    ModelState.AddModelError("", "User not found or incorrect password");
                    return View("Index", data);
                }


                HttpContext.Session.SetString("IsEnter", "true");
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);

                _logger.LogInformation("User {Email} logged in successfully", data.Email);

                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error during user authentication");
                ModelState.AddModelError("", "An error occurred during authentication");
                return View("Index", data);
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}