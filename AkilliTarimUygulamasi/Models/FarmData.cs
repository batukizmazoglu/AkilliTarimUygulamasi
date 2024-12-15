using System;

namespace AkilliTarimUygulamasi.Models
{
    public class FarmData
    {
        public int Id { get; set; }
        public decimal SoilMoisture { get; set; } // Toprak nemi
        public string Weather { get; set; } // Hava durumu
        public decimal CropYield { get; set; } // Ürün verimliliği
        public string IrrigationStatus { get; set; } // Sulama durumu
        public DateTime Date { get; set; } // Kayıt tarihi
    }
}