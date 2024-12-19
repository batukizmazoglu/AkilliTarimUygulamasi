using AkilliTarimUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkilliTarimUygulamasi.Controllers
{
    [Route("Crop")]
    public class CropController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CropController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("List")]
        public IActionResult Index()
        {
            var crops = _dbContext.Crops.Include(c => c.Field).ToList();
            if (!crops.Any())
            {
                ViewBag.Message = "Hiç ürün bulunamadı.";
            }
            return View("/Views/Crop/Index.cshtml");
            // Görünüm adı burada "Index" varsayılıyor.
        }

        // Yeni ürün ekleme sayfası
        [HttpGet("Add")]
        public IActionResult Add()
        {
            ViewBag.Fields = _dbContext.Fields.ToList(); // Tarla listesi
            return View();
        }

        // Yeni ürün ekleme işlemi
        [HttpPost("Add")]
        public IActionResult Add(Crop crop)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Geçersiz veri girişi.";
                ViewBag.Fields = _dbContext.Fields.ToList();
                return View(crop);
            }

            _dbContext.Crops.Add(crop);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // Ürün düzenleme sayfası
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var crop = _dbContext.Crops.Include(c => c.Field).FirstOrDefault(c => c.Id == id);
            if (crop == null)
            {
                return NotFound();
            }

            ViewBag.Fields = _dbContext.Fields.ToList();
            return View(crop);
        }

        // Ürün düzenleme işlemi
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Crop crop)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Geçersiz veri girişi.";
                ViewBag.Fields = _dbContext.Fields.ToList();
                return View(crop);
            }

            _dbContext.Crops.Update(crop);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // Ürün silme işlemi
        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var crop = _dbContext.Crops.FirstOrDefault(c => c.Id == id);
            if (crop == null)
            {
                return NotFound();
            }

            _dbContext.Crops.Remove(crop);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
