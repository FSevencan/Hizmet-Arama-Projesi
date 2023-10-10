using Proje.Models;

namespace Proje.ViewModels
{
    public class Uzman_YorumViewModel
    {
        public UzmanProfil? UzmanProfil { get; set; }
        public UzmanYorum? UzmanYorum { get; set; } = new UzmanYorum();
        public List<UzmanProfil>? UzmanProfiller { get; set; }
        public List<UzmanYorum>? UzmanYorumlar { get; set; }
        public Dictionary<int, int> YildizDagilimi { get; set; } = new Dictionary<int, int>();
    }
}
