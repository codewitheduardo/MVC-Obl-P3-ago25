using System.Diagnostics;
using HTTPCLIENTE_M3C_IMEM.Filters;
using HTTPCLIENTE_M3C_IMEM.Models;
using Microsoft.AspNetCore.Mvc;

namespace HTTPCLIENTE_M3C_IMEM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
