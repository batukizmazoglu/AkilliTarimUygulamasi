namespace AkilliTarimUygulamasi.Models
{
    public class Crop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public double Quantity { get; set; }
        public string Status { get; set; }

        // Foreign Key
        public int FieldId { get; set; }
        public Field Field { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
    }
}