using Microsoft.AspNetCore.Identity;

namespace Proje.Models
{
    public class AppUser : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string? Picture { get; set; }
        public string? VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiration { get; set; }
        public bool IsVerified { get; set; }
        public virtual ICollection<Mesaj> GonderilenMesajlar { get; set; }
        public virtual ICollection<Mesaj> AlinanMesajlar { get; set; }


    }
}