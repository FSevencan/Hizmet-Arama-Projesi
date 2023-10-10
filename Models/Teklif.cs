namespace Proje.Models
{
	public class Teklif
	{
		public int TeklifId { get; set; }
		public string Baslik { get; set; }
		public string Aciklama { get; set; }
		public string? Fotograf { get; set; }
		public int MusteriId { get; set; }
		public virtual MusteriProfil Musteri { get; set; }
		public int? UzmanProfilId { get; set; }
		public virtual UzmanProfil? Uzman { get; set; }
		public int? AltKategoriId { get; set; }
		public virtual AltKategori? AltKategori { get; set; }
		public string? Durum { get; set; }
		public DateTime ZamanDamgasi { get; set; }


		// TEKLIF SAYFASINDA MUSTERI ADRES DEGISIKLIGI YAPABILIR DIYE EKLENDI //
		public int? SehirId { get; set; }  
		public virtual Sehir Sehir { get; set; }  

		public int? IlceId { get; set; }  
		public virtual Ilce Ilce { get; set; }  

		public virtual ICollection<Mesaj> Mesajlar { get; set; }  
	}
}
