using System.Linq;
using CVHub.Data;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CVHub.Controllers
{
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly AppDbContext _context;

        public RegistrationController(AppDbContext context, ILogger<RegistrationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            _logger.LogInformation("Пользователь открыл страницу регистрации в {time}", DateTime.Now);
            ViewBag.IsEnter = HttpContext.Session.GetString("IsEnter") == "true";
            return View();
        }

        [HttpPost("Check")]
        public IActionResult Check(Registration data)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == data.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email уже используется");
                return View("Index");
            }

            _context.Users.Add(data);
            _context.SaveChanges();


            HttpContext.Session.SetInt32("UserId", data.Id);
            HttpContext.Session.SetString("UserName", data.Name);
            HttpContext.Session.SetString("IsEnter", "true");

            return RedirectToAction("Index", "Account");
        }

        [HttpGet("Authorisation")]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Authorisation");
            // return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("", "Неверный email или пароль");
                return View();
            }


            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("IsEnter", "true");

            return RedirectToAction("Index", "Account");
        }


        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Произошла ошибка!");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}