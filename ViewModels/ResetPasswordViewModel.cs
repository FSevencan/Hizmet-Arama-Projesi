using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Yeni şifre :")]
		[MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
		public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler aynı değil.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Yeni şifre Tekrar :")]
        public string PasswordConfirm { get; set; } = null!;
    }
}