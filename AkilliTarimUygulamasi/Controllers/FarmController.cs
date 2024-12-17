using Microsoft.AspNetCore.Mvc;
using AkilliTarimUygulamasi.Services; // Servis katmanına erişim
using AkilliTarimUygulamasi.Models;  // Model erişimi

namespace AkilliTarimUygulamasi.Controllers;
[Route("Farm")]
public class FarmController : Controller
{
    private readonly IFarmDataService _farmDataService;

    public FarmController(IFarmDataService farmDataService)
    {
        _farmDataService = farmDataService;
    }

    // Tarım verilerini listeleme - View için
    [HttpGet("List")]
    public IActionResult Index()
    {
        var farmData = _farmDataService.GetAllFarmData();
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
        return View();
    }

    // Yeni tarım verisi ekleme işlemi
    [HttpPost("Add")]
    public IActionResult Add([FromForm] FarmData farmData)
    {
        if (!ModelState.IsValid || farmData == null)
        {
            ViewBag.Message = "Geçersiz veri girişi.";
            return View();
        }

        _farmDataService.AddFarmData(farmData);
        return RedirectToAction("Index");
    }
}
