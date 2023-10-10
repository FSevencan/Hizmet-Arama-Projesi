using Proje.Models;

namespace Proje.ViewModels
{
    public class TekliflerViewModel
    {
        public List<Teklif> Teklifler { get; set; }
        public string CurrentUserId { get; set; }
    }
}
