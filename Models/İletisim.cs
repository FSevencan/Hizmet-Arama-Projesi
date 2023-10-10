namespace Proje.Models
{
    public class İletisim
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }
        public DateTime GonderilmeTarihi { get; set; }
        public DateTime? OkunmaTarihi { get; set; }
    }
}
