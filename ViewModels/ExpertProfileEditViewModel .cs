using Proje.Models;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class ExpertProfileEditViewModel
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
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

        public int? SaatlikUcret { get; set; }

        [Display(Name = "Hizmet Vereceğiniz İl :")]
        public int? SehirId { get; set; }

        [Display(Name = "Hizmet Vereceğiniz İlçe :")]
        public int? IlceId { get; set; }

        [Display(Name = "Profil Resmi :")]
        public IFormFile? Picture { get; set; }
        public string? PictureShow { get; set; }

        [Display(Name = "Title :")]
        public string? Title { get; set; }

        [Display(Name = "Aciklama :")]
        public string? Aciklama { get; set; }

        public List<Kategori>? Kategoriler { get; set; }
        public List<AltKategori>? AltKategoriler { get; set; }

        public int? KategoriId { get; set; }
        public int? AltKategoriId { get; set; }

        public List<Sehir>? Sehirler { get; set; }
        public List<Ilce>? Ilceler { get; set; }

        
    }
}