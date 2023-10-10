namespace Proje.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        public string Isim { get; set; }
        public string? Fotograf { get; set; }
        public virtual ICollection<AltKategori>? AltKategoriler { get; set; }
        
    }
}
