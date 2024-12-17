using AkilliTarimUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AkilliTarimUygulamasi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Giriş sayfasını görüntüle
        public IActionResult Index()
        {
            return View();
        }

        // Kullanıcıyı doğrulayıp giriş yapmasını sağla
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Email ve şifreyi doğrula
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                // Hatalı giriş
                ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre.";
                return View("Index");
            }

            // Kullanıcıyı session'a kaydet
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Admin mi kontrol et
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            // Normal kullanıcıyı yönlendir
            return RedirectToAction("Index", "Home");
        }

        // Çıkış işlemi
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}