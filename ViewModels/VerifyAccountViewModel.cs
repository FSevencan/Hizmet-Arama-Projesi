using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class VerifyAccountViewModel
    {
        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        [Display(Name = "E-posta Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Doğrulama kodu gereklidir.")]
        [Display(Name = "Doğrulama Kodu")]
        public string VerificationCode { get; set; }
    }
}
