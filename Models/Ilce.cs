namespace Proje.Models
{
	public class Ilce
	{
		public int IlceId { get; set; }
		public string? IlceAdi { get; set; }
		public int SehirId { get; set; }
		public virtual Sehir Sehir { get; set; }
	}
}
