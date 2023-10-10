using Proje.Models;

namespace Proje.ViewModels
{
    public class MesajViewModel
    {
        public List<AppUser>? Kullanicilar { get; set; }
        public List<Mesaj> Mesajlar { get; set; }
        public string SecilenKullaniciId { get; set; }
        public int? TeklifId { get; set; }
        public string CurrentUserId { get; set; }
		public Dictionary<string, DateTime> KullaniciSonMesajZamanlari { get; set; } = new Dictionary<string, DateTime>();


	}
}
