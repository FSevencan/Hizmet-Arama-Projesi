using System.Reflection.Metadata;

namespace Proje.Models
{
    public class CokluListeleme
    {
        
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<UzmanProfil> UzmanProfilleri { get; set; }
        public IEnumerable<AltKategori> AltKategoriler { get; set; }
        public IEnumerable<MusteriProfil> MusteriProfilleri { get; set; }
        public IEnumerable<Teklif> Teklifler{ get; set; }
        public IEnumerable<UzmanYorum> UzmanYorumlari{ get; set; }
       
        
       
        public IEnumerable<AppUser> Users { get; set; }
    }
}
