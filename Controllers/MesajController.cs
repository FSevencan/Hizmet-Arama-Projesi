using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.ViewModels;

namespace Proje.Controllers
{
    public class MesajController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MesajController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

		public async Task<IActionResult> Index(string id = null)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var mesajlar = _context.Mesajlar
				.Include(m => m.Gonderen)
				.Include(m => m.Teklif)
				.Include(m => m.Alan)
				.Where(m => m.GonderenId == currentUser.Id || m.AlanId == currentUser.Id)
				.OrderBy(m => m.ZamanDamgasi)
				.ToList();

			var kullanicilar = mesajlar
				.Select(m => m.GonderenId == currentUser.Id ? m.Alan : m.Gonderen)
				.Distinct()
				.ToList();

			AppUser seciliKullanici = null;

			// Eğer seçili kullanıcı varsa ve bu kullanıcı ile herhangi bir mesajlaşma geçmişi yoksa,
			// bu kullanıcıyı 'kullanicilar' listesine ekle.
			if (id != null)
			{
				seciliKullanici = await _userManager.FindByIdAsync(id);

				if (seciliKullanici != null && !kullanicilar.Any(k => k.Id == id))
				{
					kullanicilar.Add(seciliKullanici);
				}
			}

			// Kullanıcıların tek tek son mesajlaşma saatleri tespit et ve göster
			var kullaniciSonMesajZamanlari = new Dictionary<string, DateTime>();
			foreach (var kullanici in kullanicilar)
			{
				var sonMesajZamani = _context.Mesajlar
					.Where(m => (m.GonderenId == kullanici.Id && m.AlanId == currentUser.Id)
							|| (m.GonderenId == currentUser.Id && m.AlanId == kullanici.Id))
					.OrderByDescending(m => m.ZamanDamgasi)
					.FirstOrDefault()?.ZamanDamgasi;

				kullaniciSonMesajZamanlari[kullanici.Id] = sonMesajZamani ?? DateTime.Now;
			}

			var viewModel = new MesajViewModel
			{
				Kullanicilar = kullanicilar,
				Mesajlar = seciliKullanici != null ? mesajlar.Where(m => m.GonderenId == seciliKullanici.Id || m.AlanId == seciliKullanici.Id).ToList() : new List<Mesaj>(),
				SecilenKullaniciId = id,
				CurrentUserId = currentUser.Id,
				KullaniciSonMesajZamanlari = kullaniciSonMesajZamanlari,
			};

			return View(viewModel);
		}


		public IActionResult Gonder(string id)
		{
			var mesaj = new Mesaj
			{
				AlanId = id,  // Mesajın alıcısının ID'si
				GonderenId = _userManager.GetUserId(User)  // Şu anki kullanıcının ID'si
			};

			return View(mesaj);
		}

		[HttpPost]
		public async Task<IActionResult> Gonder(Mesaj mesaj, int? teklifId = null) // teklifId nullable hale getirildi.
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null)
			{
				return RedirectToAction("SignIn", "User");
			}

			if (teklifId.HasValue) // TeklifId değeri var mı kontrol ediliyor.
			{
				var teklif = _context.Teklifler.FirstOrDefault(t => t.TeklifId == teklifId.Value);
				if (teklif == null)
				{
					// Teklif bulunamadı, hata mesajı gösterilebilir veya başka bir işlem yapılabilir.
					return View(mesaj);
				}

				var musteri = await _context.Users.FirstOrDefaultAsync(u => u.Id == teklif.MusteriId.ToString());
				var uzman = await _context.Users.FirstOrDefaultAsync(u => u.Id == teklif.UzmanProfilId.ToString());

				if (musteri == null || uzman == null)
				{
					// Musteri veya Uzman bulunamadı, hata mesajı gösterilebilir veya başka bir işlem yapılabilir.
					return View(mesaj);
				}

				mesaj.GonderenId = currentUser.Id;
				mesaj.AlanId = currentUser.Id == musteri.Id ? uzman.Id : musteri.Id;
				mesaj.ZamanDamgasi = DateTime.Now;
				mesaj.TeklifId = teklifId.Value;  // Eğer Mesaj modelinde TeklifId adında bir özellik oluşturduysanız.

				_context.Mesajlar.Add(mesaj);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index", new { id = mesaj.AlanId });
			}
			else  // TeklifId null ise doğrudan mesajlaşma gerçekleşiyor.
			{
				// Gerekli işlemler burada yapılabilir. Mesela:
				mesaj.GonderenId = currentUser.Id;
				mesaj.ZamanDamgasi = DateTime.Now;

				_context.Mesajlar.Add(mesaj);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index", new { id = mesaj.AlanId });
			}
		}
	}
}

