using System.ComponentModel.DataAnnotations;

namespace AkilliTarimUygulamasi.Models.viewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ad Soyad gereklidir")]
    [Display(Name = "Ad Soyad")]
    public string Name { get; set; }

    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir")]
    [StringLength(100, ErrorMessage = "Şifre en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; }
}