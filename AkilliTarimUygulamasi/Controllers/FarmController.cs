using Microsoft.AspNetCore.Mvc;
using AkilliTarimUygulamasi.Services; // Servis katmanına erişim
using AkilliTarimUygulamasi.Models;  // Model erişimi
using System.Linq; // Veri filtrelemeleri için LINQ

namespace AkilliTarimUygulamasi.Controllers;

[Route("Farm")]
public class FarmController : Controller
{
    private readonly IFarmDataService _farmDataService;
    private readonly ApplicationDbContext _dbContext;

    public FarmController(IFarmDataService farmDataService, ApplicationDbContext dbContext)
    {
        _farmDataService = farmDataService;
        _dbContext = dbContext;
    }

    // Tarım verilerini listeleme - View için
    [HttpGet("List")]
    public IActionResult Index()
    {
        var farmData = _dbContext.FarmData.ToList(); // Veritabanından tüm tarla verilerini alır
        if (farmData == null || !farmData.Any())
        {
            ViewBag.Message = "Hiç tarla verisi bulunamadı.";
            return View(new List<FarmData>());
        }
        return View(farmData);
    }

    // Yeni tarım verisi ekleme sayfası
    [HttpGet("Add")]
    public IActionResult Add()
    {
        ViewBag.Fields = _dbContext.Fields.ToList(); // Tarla seçeneklerini doldurur
        return View();
    }

    // Yeni tarım verisi ekleme işlemi
    [HttpPost("Add")]
    public IActionResult Add([FromForm] FarmData farmData)
    {
        if (!ModelState.IsValid || farmData == null)
        {
            ViewBag.Message = "Geçersiz veri girişi.";
            ViewBag.Fields = _dbContext.Fields.ToList(); // Hatalı durumda tarla listesini tekrar doldur
            return View();
        }

        _dbContext.FarmData.Add(farmData);
        _dbContext.SaveChanges(); // Veriyi kaydet
        return RedirectToAction("Index");
    }

    // Tarla verisi düzenleme sayfası
    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var farmData = _dbContext.FarmData.FirstOrDefault(f => f.Id == id);
        if (farmData == null)
        {
            return NotFound("Veri bulunamadı.");
        }

        ViewBag.Fields = _dbContext.Fields.ToList(); // Tarla listesini sağlar
        return View(farmData);
    }

    // Tarla verisi düzenleme işlemi
    [HttpPost("Edit/{id}")]
    public IActionResult Edit(int id, [FromForm] FarmData updatedFarmData)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Geçersiz veri girişi.";
            ViewBag.Fields = _dbContext.Fields.ToList();
            return View(updatedFarmData);
        }

        var farmData = _dbContext.FarmData.FirstOrDefault(f => f.Id == id);
        if (farmData == null)
        {
            return NotFound("Veri bulunamadı.");
        }

        farmData.Name = updatedFarmData.Name;
        farmData.Area = updatedFarmData.Area;
        farmData.Crop = updatedFarmData.Crop;
        farmData.CreatedAt = updatedFarmData.CreatedAt;
        farmData.FieldId = updatedFarmData.FieldId;

        _dbContext.SaveChanges(); // Güncellemeyi kaydet
        return RedirectToAction("Index");
    }

    // Tarla verisi silme işlemi
    [HttpPost("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var farmData = _dbContext.FarmData.FirstOrDefault(f => f.Id == id);
        if (farmData == null)
        {
            return NotFound("Veri bulunamadı.");
        }

        _dbContext.FarmData.Remove(farmData);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}