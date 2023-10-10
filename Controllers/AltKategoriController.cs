using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
	public class AltKategoriController : Controller
	{
		private readonly AppDbContext _context;

		public AltKategoriController(AppDbContext context)
		{
			_context = context;
		}

        


		[Route("Kategori/{isim}")]
        public async Task<IActionResult> Index(string isim, int sayfaNo = 1, int sayfaBoyutu = 6)
        {
            var kategori = await _context.Kategoriler
                .Include(a => a.AltKategoriler)
                .FirstOrDefaultAsync(k => k.Isim == isim);

            if (kategori == null)
            {
                return NotFound();
            }

            
            // Toplam sayfa sayısını hesapla
            var toplamSayfaSayisi = (int)Math.Ceiling(kategori.AltKategoriler.Count / (double)sayfaBoyutu);

            // AltKategorileri sayfala
            int skip = (sayfaNo - 1) * sayfaBoyutu;
            var altKategoriler = kategori.AltKategoriler.Skip(skip).Take(sayfaBoyutu).ToList();

            // Toplam sayfa sayısını view'a aktar
            ViewBag.ToplamSayfaSayisi = toplamSayfaSayisi;
            ViewBag.SayfaNo = sayfaNo;
            ViewBag.Isim = isim;

            return View(altKategoriler);
        }
    }
}
