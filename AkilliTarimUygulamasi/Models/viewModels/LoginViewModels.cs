using System.ComponentModel.DataAnnotations;

namespace AkilliTarimUygulamasi.Models.viewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}