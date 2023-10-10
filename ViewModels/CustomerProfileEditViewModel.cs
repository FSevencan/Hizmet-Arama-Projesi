using Proje.Models;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
	public class CustomerProfileEditViewModel
	{
		[Required(ErrorMessage = "Kullanıcı Ad alanı boş bırakılamaz.")]
		[Display(Name = "Ad :")]
		public string Ad { get; set; }

		[Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
		[Display(Name = "Soyad :")]
		public string Soyad { get; set; }

		[Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
		[Display(Name = "Email :")]
		public string Email { get; set; }

		[Display(Name = "Telefon :")]
		public string? Telefon { get; set; }

		[Display(Name = "İl :")]
		public int? SehirId { get; set; }

		[Display(Name = "İlçe :")]
		public int? IlceId { get; set; }

		[Display(Name = "PostaKodu :")]
		public string? PostaKodu { get; set; }

		[Display(Name = "Adres :")]
		public string? Adres { get; set; }

		[Display(Name = "Profil resmi :")]
		public IFormFile? Picture { get; set; }
		public string? PictureShow { get; set; }

		public List<Sehir>? Sehirler { get; set; }
		public List<Ilce>? Ilceler { get; set; }
	}
}