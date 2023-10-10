using Proje.Models;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
	public class KategoriTeklifViewModel
	{
		[Required(ErrorMessage = "Başlık gereklidir.")]
		[StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
		public string Baslik { get; set; }

		[Required(ErrorMessage = "Açıklama gereklidir.")]
		[StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
		public string Aciklama { get; set; }
		public string? SaatlikUcret { get; set; }

		public int MusteriId { get; set; }

		public int? UzmanId { get; set; }
		public DateTime Tarih { get; set; }

		public int? MinSaatlikUcret { get; set; }
		public int? MaxSaatlikUcret { get; set; }
		public IFormFile? Fotograf { get; set; }

		public string? AltKategoriAdi { get; set; }
		public int AltKategoriId { get; set; }

		[Display(Name = "İl :")]
        public int? SehirId { get; set; }

        [Display(Name = "İlçe :")]
        public int? IlceId { get; set; }

        public List<Sehir>? Sehirler { get; set; }
        public List<Ilce>? Ilceler { get; set; }
    }
}
