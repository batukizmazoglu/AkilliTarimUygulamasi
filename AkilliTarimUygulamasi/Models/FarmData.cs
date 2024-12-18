using System;
using System.ComponentModel.DataAnnotations;

namespace AkilliTarimUygulamasi.Models
{
    public class FarmData
    {
        public int Id { get; set; } // Birincil Anahtar

        [Required]
        public string Name { get; set; } = string.Empty; // Tarla Adı

        [Required]
        public double Area { get; set; } // Alan (m²)

        [Required]
        public string Crop { get; set; } = string.Empty; // Ürün Adı

        public DateTime Date { get; set; } = DateTime.Now; // Tarih Bilgisi
        public double SoilMoisture { get; set; } // Toprak Nem Oranı
        public string Weather { get; set; } = string.Empty; // Hava Durumu
        public double CropYield { get; set; } // Ürün Verimi

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Oluşturulma Tarihi

        // Foreign Key ve Navigation Property
        [Required]
        public int FieldId { get; set; } // Tarlayı tanımlayan yabancı anahtar
        public Field Field { get; set; } // İlgili tarla için ilişki
    }
}