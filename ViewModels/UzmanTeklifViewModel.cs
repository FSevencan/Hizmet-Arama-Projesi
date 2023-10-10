using Proje.Models;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class UzmanTeklifViewModel
    {
        [Required(ErrorMessage = "Başlık gereklidir.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string Aciklama { get; set; }
        public string? SaatlikUcret { get; set; }

        public int MusteriId { get; set; }
        public int UzmanId { get; set; }
        public string? UzmanAdi{ get; set; }
        public DateTime Tarih { get; set; }


        public IFormFile? Fotograf { get; set; }

        public string? AltKategoriAdi { get; set; }
        public int? AltKategoriId { get; set; }

    }
}
