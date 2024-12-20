using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using System;
using System.Linq;
using System.Text;

namespace AkilliTarimUygulamasi.Services
{
    public class FarmReportService
    {
        private readonly IFarmDataService _farmDataService;

        public FarmReportService(IFarmDataService farmDataService)
        {
            _farmDataService = farmDataService;
        }

        // Rapor oluşturma
        public string GenerateFarmReport(DateTime startDate, DateTime endDate)
        {
            var farmData = _farmDataService.GetAllFarmData()
                .Where(f => f.Date >= startDate && f.Date <= endDate);

            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Farm Data Report");
            reportBuilder.AppendLine("Date\tSoil Moisture\tWeather\tCrop Yield");

            foreach (var data in farmData)
            {
                reportBuilder.AppendLine($"{data.Date:yyyy-MM-dd}\t{data.SoilMoisture}\t{data.Weather}\t{data.CropYield}");
            }

            return reportBuilder.ToString();
        }

        // Ürün adına göre rapor
        public string GenerateCropSpecificReport(string cropName)
        {
            var farmData = _farmDataService.GetFarmDataByCrop(cropName);

            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine($"Farm Data Report for Crop: {cropName}");
            reportBuilder.AppendLine("Date\tField Name\tArea\tCrop Yield");

            foreach (var data in farmData)
            {
                reportBuilder.AppendLine($"{data.Date:yyyy-MM-dd}\t{data.Name}\t{data.Area}\t{data.CropYield}");
            }

            return reportBuilder.ToString();
        }
    }
}