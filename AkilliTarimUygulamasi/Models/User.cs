namespace AkilliTarimUygulamasi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    // Navigation Property
    public ICollection<Field> Fields { get; set; } = new List<Field>();  // Varsayılan boş liste atandı
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();    // Varsayılan boş liste atandı
}