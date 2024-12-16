using System.Diagnostics;
using AkilliTarimUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AkilliTarimUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index action - the default home page
        public IActionResult Index()
        {
            _logger.LogInformation("User accessed the Home page.");
            ViewData["Message"] = "Welcome to the Home page!";
            return View();
        }

        // Privacy action - a privacy page
        public IActionResult Privacy()
        {
            _logger.LogInformation("User accessed the Privacy page.");
            return View();
        }

        // Error action - displays error details
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError("An error occurred. Request ID: {RequestId}", requestId);
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}