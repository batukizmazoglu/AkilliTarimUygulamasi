namespace AkilliTarimUygulamasi.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double Amount { get; set; }
        public double QuantitySold { get; set; }

        // Foreign Key for User (Who made the sale)
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign Key for Crop (Which crop was sold)
        public int CropId { get; set; }
        public Crop Crop { get; set; }  // Navigation Property for the related crop

        // Foreign Key for Field (Which field was the crop from)
        public int FieldId { get; set; }
        public Field Field { get; set; }
        

    }
}