namespace Proje.Models
{
    public class UzmanYorum
    {
        public int UzmanYorumId { get; set; }
        public string? Yorum { get; set; }
        public DateTime? YorumTarihi { get; set; }
        public int? Derece { get; set; }
        public int? UzmanProfilId { get; set; } 
        public virtual UzmanProfil? Uzman { get; set; }
        public int MusteriId { get; set; } 
        public virtual MusteriProfil? Musteri { get; set; }
    }
}
