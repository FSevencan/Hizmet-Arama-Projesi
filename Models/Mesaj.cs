namespace Proje.Models
{
	public class Mesaj
	{
		public int MesajId { get; set; }
		public string Icerik { get; set; }
		public DateTime ZamanDamgasi { get; set; }

		public string GonderenId { get; set; }
		public virtual AppUser Gonderen { get; set; }

		public string AlanId { get; set; }
		public virtual AppUser Alan { get; set; }

		public int? TeklifId { get; set; }  
		public virtual Teklif Teklif { get; set; }  
	}
}
