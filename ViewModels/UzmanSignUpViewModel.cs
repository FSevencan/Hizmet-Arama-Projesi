using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class UzmanSignUpViewModel
    {
        public UzmanSignUpViewModel()
        {
        }

        public UzmanSignUpViewModel(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        [Display(Name = "Ad :")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        [Display(Name = "Soyad :")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordConfirm { get; set; }

        
    }
}

