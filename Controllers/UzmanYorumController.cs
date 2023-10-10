using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using Proje.ViewModels;
using System.Security.Claims;

namespace Proje.Controllers
{
	public class UzmanYorumController : Controller
	{
		private readonly AppDbContext _context;

        public UzmanYorumController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Uzman_YorumViewModel viewModel)
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (kullaniciId == null)
            {
                return RedirectToAction("SignIn", "User");
            }

            UzmanYorum yorum = viewModel.UzmanYorum;

            if (ModelState.IsValid)
            {
                var uzman = await _context.UzmanProfilleri.FindAsync(yorum.UzmanProfilId);
                var musteri = await _context.MusteriProfilleri
                                            .Include(m => m.Kullanici)
                                            .FirstOrDefaultAsync(m => m.KullaniciId == kullaniciId);

                if (uzman == null || musteri == null || musteri.Kullanici == null)
                {
                    return NotFound();
                }

                yorum.Uzman = uzman;
                yorum.Musteri = musteri;
                yorum.YorumTarihi = DateTime.Now;
              

                string musteriProfilResmi = musteri.Kullanici.Picture;

                _context.Add(yorum);
                await _context.SaveChangesAsync();

                // Yıldız dağılımını oluştur
                Dictionary<int, int> yildizDagilimi = new Dictionary<int, int>();
                for (int i = 1; i <= 5; i++)
                {
                    yildizDagilimi[i] = await _context.UzmanYorumlari.CountAsync(y => y.UzmanProfilId == yorum.UzmanProfilId && y.Derece == i);
                }
                // Yorum sayısı, ortalama derece ve yıldız dağılımı bilgilerini çek
                int yorumSayisi = await _context.UzmanYorumlari.CountAsync(y => y.UzmanProfilId == yorum.UzmanProfilId);
                double ortalamaDerece = await _context.UzmanYorumlari.Where(y => y.UzmanProfilId == yorum.UzmanProfilId).AverageAsync(y => (double?)y.Derece) ?? 0;

                return Json(new
                {
                    success = true,
                    message = "Yorum başarıyla kaydedildi.",
                    yorum = yorum.Yorum,
                    musteriProfilResmi = musteriProfilResmi,
                    musteriAd = musteri.Ad,
                    musteriSoyad = musteri.Soyad,
                    derece = yorum.Derece,
                    yorumTarihi = yorum.YorumTarihi,
                    yorumSayisi = yorumSayisi,
                    ortalamaDerece = ortalamaDerece,
                    yildizDagilimi = yildizDagilimi
                });

            }

         
            return Json(new { success = false, message = "Bir hata oluştu." });
        }
    }
}

