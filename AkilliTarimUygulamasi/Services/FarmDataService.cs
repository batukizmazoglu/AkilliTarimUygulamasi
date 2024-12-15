using AkilliTarimUygulamasi.Models; // Veritabanı erişimi
using System.Collections.Generic;
using System.Linq;
using AkilliTarimUygulamasi.Models;

namespace AkilliTarimUygulamasi.Services
{
    public interface IFarmDataService
    {
        IEnumerable<FarmData> GetAllFarmData();
        void AddFarmData(FarmData farmData);
    }

    public class FarmDataService : IFarmDataService
    {
        private readonly ApplicationDbContext _context;

        public FarmDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FarmData> GetAllFarmData()
        {
            return _context.FarmData.ToList();
        }

        public void AddFarmData(FarmData farmData)
        {
            _context.FarmData.Add(farmData);
            _context.SaveChanges();
        }
    }
}