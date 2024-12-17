using Microsoft.AspNetCore.Mvc;

namespace AkilliTarimUygulamasi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Admin sayfası için bir view döndür
            return View();
        }
    }
}