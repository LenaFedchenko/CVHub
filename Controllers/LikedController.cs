using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CVHub.Controllers
{
    [Route("[controller]")]
    public class LikedController : Controller
    {
        private readonly ILogger<LikedController> _logger;

        public LikedController(ILogger<LikedController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }


    }
}