namespace Proje.Models
{
	public class Sehir
	{
		public int SehirId { get; set; }
		public string? SehirAdi { get; set; }
		public virtual ICollection<Ilce> Ilceler { get; set; }
	}
}
