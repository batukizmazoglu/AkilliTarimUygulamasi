using AkilliTarimUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkilliTarimUygulamasi.Controllers
{
    [Route("Sales")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SalesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Satışları listeleme
        [HttpGet("List")]
        public IActionResult Index()
        {
            var sales = _dbContext.Sales
                .Include(s => s.Crop)
                .Include(s => s.User)
                .Include(s => s.Field)
                .ToList();

            if (!sales.Any())
            {
                ViewBag.Message = "Hiç satış bulunamadı.";
            }
            return View(sales);// burada sales yazıyordu hata veriyor diye boş döndsün dedim şu anlık 
        }

        // Yeni satış ekleme sayfası
        [HttpGet("Add")]
        public IActionResult Add()
        {
            ViewBag.Crops = _dbContext.Crops.ToList();
            ViewBag.Fields = _dbContext.Fields.ToList();
            ViewBag.Users = _dbContext.Users.ToList();
            return View();
        }

        // Yeni satış ekleme işlemi
        [HttpPost("Add")]
        public IActionResult Add(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Geçersiz veri girişi.";
                ViewBag.Crops = _dbContext.Crops.ToList();
                ViewBag.Fields = _dbContext.Fields.ToList();
                ViewBag.Users = _dbContext.Users.ToList();
                return View(sale);
            }

            _dbContext.Sales.Add(sale);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}