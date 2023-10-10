namespace Proje.Extenisons
{
    public static class DateTimeExtensions
    {
        public static string ZamandanFark(this DateTime dateTime)
        {
            var fark = DateTime.Now - dateTime;

            if (fark.TotalMinutes < 60)
            {
                return $"{fark.TotalMinutes:0} dakika önce";
            }
            else if (fark.TotalHours < 24)
            {
                return $"{fark.TotalHours:0} saat önce";
            }
            else if (fark.TotalDays < 7)
            {
                return $"{fark.TotalDays:0} gün önce";
            }
            else
            {
                return dateTime.ToShortDateString(); // Eğer 1 haftadan daha eskiyse, tarihi göster
            }
        }
    }
}
