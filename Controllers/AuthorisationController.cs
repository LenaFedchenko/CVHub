using System.Linq;
using System.Threading.Tasks;
using CVHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CVHub.Data;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckUser(Authorisation data)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == data.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    return View("Index");
                }

                if (user.Password != data.Password)
                {
                    ModelState.AddModelError("", "Неверный пароль");
                    return View("Index");
                }

                HttpContext.Session.SetString("IsEnter", "true");
                return RedirectToAction("Index", "Home");
            }

            return View("Index");
        }

    }
}
