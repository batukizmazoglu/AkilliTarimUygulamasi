using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using SoapCore;  // SoapCore için gerekli namespace

namespace AkilliTarimUygulamasi.Services
{
    // SoapService ve SoapMethod gibi dekoratörleri kaldırıyoruz.
    public class FarmReportService
    {
        private readonly IFarmDataService _farmDataService;

        public FarmReportService(IFarmDataService farmDataService)
        {
            _farmDataService = farmDataService;
        }

        // Bu metod artık SOAP endpoint'i olarak çalışacak.
        public string GenerateFarmReport(DateTime startDate, DateTime endDate)
        {
            var farmData = _farmDataService.GetAllFarmData()
                .Where(f => f.Date >= startDate && f.Date <= endDate);

            string report = "Farm Data Report\n";
            foreach (var data in farmData)
            {
                report += $"Date: {data.Date}, Soil Moisture: {data.SoilMoisture}, Weather: {data.Weather}, Crop Yield: {data.CropYield}\n";
            }
            return report;
        }
    }
}
