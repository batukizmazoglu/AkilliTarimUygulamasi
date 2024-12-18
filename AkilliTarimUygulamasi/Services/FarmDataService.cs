using AkilliTarimUygulamasi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AkilliTarimUygulamasi.Services
{
    public class FarmDataService : IFarmDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public FarmDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Tüm tarla verilerini getirme
        public IEnumerable<FarmData> GetAllFarmData()
        {
            return _dbContext.FarmData.Include(f => f.Field).ToList();
        }

        // Belirli bir tarla verisini getirme
        public FarmData GetFarmDataById(int id)
        {
            return _dbContext.FarmData.Include(f => f.Field).FirstOrDefault(f => f.Id == id);
        }

        // Yeni tarla verisi ekleme
        public void AddFarmData(FarmData farmData)
        {
            _dbContext.FarmData.Add(farmData);
            _dbContext.SaveChanges();
        }

        // Tarla verisini güncelleme
        public void UpdateFarmData(FarmData updatedFarmData)
        {
            var existingFarmData = _dbContext.FarmData.FirstOrDefault(f => f.Id == updatedFarmData.Id);
            if (existingFarmData != null)
            {
                existingFarmData.Name = updatedFarmData.Name;
                existingFarmData.Area = updatedFarmData.Area;
                existingFarmData.Crop = updatedFarmData.Crop;
                existingFarmData.Date = updatedFarmData.Date;
                existingFarmData.SoilMoisture = updatedFarmData.SoilMoisture;
                existingFarmData.Weather = updatedFarmData.Weather;
                existingFarmData.CropYield = updatedFarmData.CropYield;
                existingFarmData.CreatedAt = updatedFarmData.CreatedAt;

                _dbContext.SaveChanges();
            }
        }

        // Tarla verisini silme
        public void DeleteFarmData(int id)
        {
            var farmData = _dbContext.FarmData.FirstOrDefault(f => f.Id == id);
            if (farmData != null)
            {
                _dbContext.FarmData.Remove(farmData);
                _dbContext.SaveChanges();
            }
        }
    }

    // Servis için arayüz
    public interface IFarmDataService
    {
        IEnumerable<FarmData> GetAllFarmData();
        FarmData GetFarmDataById(int id);
        void AddFarmData(FarmData farmData);
        void UpdateFarmData(FarmData updatedFarmData);
        void DeleteFarmData(int id);
    }
}
