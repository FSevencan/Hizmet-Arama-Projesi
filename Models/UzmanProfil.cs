using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class UzmanProfil
    {
        public int UzmanProfilId { get; set; }
        public string KullaniciId { get; set; }
        public virtual AppUser Kullanici { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string? Title { get; set; }
        public string? Aciklama { get; set; }
        public string? Telefon { get; set; }
        public DateTime? KayıtTarihi { get; set; }
		public int? SaatlikUcret { get; set; }
		public int? SehirId { get; set; }
		public virtual Sehir Sehir { get; set; }
		public int? IlceId { get; set; }
		public virtual Ilce Ilce { get; set; }
        public int?  IsBasarisi { get; set; } 
        public int? ToplamIsler { get; set; } 
        public int? ToplamSaatler { get; set; }

        public int ToplamYildiz { get; set; } = 0; 
        public int YorumSayisi { get; set; } = 0; 
		public decimal OrtalamaPuan
		{
			get
			{
				return (YorumSayisi > 0) ? (decimal)ToplamYildiz / YorumSayisi : 0;
			}
		}
		public int? KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }

        public int? AltKategoriId { get; set; }
        public virtual AltKategori AltKategori { get; set; }
        public virtual ICollection<UzmanYorum> UzmanYorumlari { get; set; }
        public virtual ICollection<Mesaj> Mesajlar { get; set; }
        public virtual ICollection<Teklif> Teklifler { get; set; }
    }
}
