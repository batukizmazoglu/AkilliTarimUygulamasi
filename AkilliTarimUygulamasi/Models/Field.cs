namespace AkilliTarimUygulamasi.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation Property
        public ICollection<Crop> Crops { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}