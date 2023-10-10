namespace Proje.Models
{
    public class TamamlanmisIs
    {
        public int TamamlanmisIsId { get; set; }
        public int UzmanProfilId { get; set; }
        public virtual UzmanProfil Uzman { get; set; }
        public int TeklifId { get; set; }
        public virtual Teklif Teklif { get; set; }
        public int Derece { get; set; }
        public string Yorum { get; set; }
    }
}
