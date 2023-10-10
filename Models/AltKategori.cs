namespace Proje.Models
{
    public class AltKategori
    {
        public int AltKategoriId { get; set; }
        public string Isim { get; set; }
        public string? Fotograf { get; set; }
        public string? Aciklama { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori? Kategori { get; set; }
       
    }
}
