namespace Proje.Models
{
    public class MusteriProfil
    {
        public int MusteriProfilId { get; set; }
        public string KullaniciId { get; set; }
        public virtual AppUser Kullanici { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string? Telefon { get; set; }
		public int? SehirId { get; set; }
		public virtual Sehir Sehir { get; set; }
		public int? IlceId { get; set; }
		public virtual Ilce Ilce { get; set; }
        public string? PostaKodu { get; set; }
        public string? Adres { get; set; }

        public virtual ICollection<Mesaj> Mesajlar { get; set; }
        public virtual ICollection<Teklif> Teklifler { get; set; }
    }
}
