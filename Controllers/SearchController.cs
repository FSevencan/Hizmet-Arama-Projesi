using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using Proje.ViewModels;
using System.Security.Claims;

namespace Proje.Controllers
{
	public class SearchController : Controller
	{
		private readonly AppDbContext _context;

		public SearchController(AppDbContext context)
		{
			_context = context;
		}


        public async Task<IActionResult> Index(string term, int? kategoriId)
        {
            // Kullanıcının il ve ilçe bilgisini al
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kullanıcı giriş yapmış mı kontrol edelim
            if (kullaniciId == null)
            {
                return RedirectToAction("SignIn", "User");
            }


            var kullaniciProfil = await _context.MusteriProfilleri
                .FirstOrDefaultAsync(m => m.KullaniciId == kullaniciId);

            if (kullaniciProfil == null)
            {
                // Kullanıcı profilini bulamazsak, bir hata mesajı gösterelim
                return NotFound();
            }

			var kullaniciIlId = kullaniciProfil.SehirId;
            var kullaniciIlceId = kullaniciProfil.IlceId;

			IQueryable<UzmanProfil> query = _context.UzmanProfilleri
				.Include(u => u.Kategori)
				.Include(u => u.AltKategori)
				.Include(u => u.Sehir)
				.Include(u => u.Ilce)
				.Include(u => u.Kullanici)
				.Where(u => u.AltKategori.Isim.Contains(term)
							&& u.SehirId == kullaniciIlId
							&& u.IlceId == kullaniciIlceId);


			// Eğer kategori seçilmişse, filtreleme yap
			if (kategoriId.HasValue)
			{
				query = query.Where(u => u.KategoriId == kategoriId);
			}

			var uzmanlar = await query.Select(u => new Uzman_SearchViewModel
			{
				Uzman = u,
				OrtalamaPuan = u.UzmanYorumlari.Any() ? u.UzmanYorumlari.Average(y => y.Derece.HasValue ? y.Derece.Value : 0) : 0,
				YorumSayisi = u.UzmanYorumlari.Count
			})
             .ToListAsync();



			if (!uzmanlar.Any()) // // Uzmanlar listesi boşsa
			{
				ViewData["UzmanYokMesaj"] = "Şu an için yakınınızda bu kategoride hizmet veren uzman bulunmamaktadır.";
			}

			return View(uzmanlar);
        }

        [HttpGet]
		public async Task<IActionResult> Search(string term, int? kategoriId)
		{
			term = term.ToLower(); // Arama terimini küçük harfe dönüştür

			// Eğer kategori seçildiyse, o kategorinin alt kategorileri içinde arama yap
			if (kategoriId.HasValue)
			{
				var altKategoriler = await _context.AltKategoriler
					.Where(a => a.KategoriId == kategoriId && a.Isim.ToLower().Contains(term)) // Küçük harf dönüşümü
					.ToListAsync();
				return Json(altKategoriler);
			}

			// Eğer kategori seçilmediyse, tüm alt kategoriler içinde arama yap
			var allAltKategoriler = await _context.AltKategoriler
				.Where(a => a.Isim.ToLower().Contains(term)) // Küçük harf dönüşümü
				.ToListAsync();
			return Json(allAltKategoriler);
		}

		
	}
}
