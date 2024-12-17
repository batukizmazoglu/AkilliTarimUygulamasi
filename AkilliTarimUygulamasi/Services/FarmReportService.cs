using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using System;
using System.Linq;

namespace AkilliTarimUygulamasi.Services
{
    public class FarmReportService
    {
        private readonly IFarmDataService _farmDataService;

        public FarmReportService(IFarmDataService farmDataService)
        {
            _farmDataService = farmDataService;
        }

        public string GenerateFarmReport(DateTime startDate, DateTime endDate)
        {
            var farmData = _farmDataService.GetAllFarmData()
                .Where(f => f.Date >= startDate && f.Date <= endDate);

            string report = "Farm Data Report\n";
            foreach (var data in farmData)
            {
                report += $"Date: {data.Date}, Soil Moisture: {data.SoilMoisture}, " +
                          $"Weather: {data.Weather}, Crop Yield: {data.CropYield}\n";
            }
            return report;
        }
    }
}