using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.ViewModels;

namespace Proje.Controllers
{
    public class TeklifController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;
        public TeklifController(AppDbContext context, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _context = context;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }


        [Route("Teklif/{isim}")]
        public async Task<IActionResult> KategoriTeklif(string isim)
        {
            isim = Uri.UnescapeDataString(isim).Replace("-", " ");

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "User"); // Kullanıcı oturum açmamışsa.
            }

            var musteri = _context.MusteriProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

            var altKategori = _context.AltKategoriler.FirstOrDefault(ak => ak.Isim == isim);

            if (altKategori == null)
            {
                return NotFound(); // Girilen ID ile eşleşen alt kategori bulunamadı.
            }

            // Uzmansa teklif butonuna basınca AnaSayfaya geri göndersin
            // Teklif Al özelliği sadece Müşteriler için
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (roles.Contains("Uzman"))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new KategoriTeklifViewModel
            {
                MusteriId = musteri.MusteriProfilId,
                SehirId = musteri?.SehirId,
                IlceId = musteri?.IlceId,
                AltKategoriId = altKategori.AltKategoriId,
                AltKategoriAdi = altKategori.Isim,

                Sehirler = _context.Sehirler.ToList(),
                Ilceler = musteri != null ? _context.Ilceler.Where(ilce => ilce.SehirId == musteri.SehirId).ToList() : new List<Ilce>()
            };

            // Bu kategorideki tüm uzmanların saatlik ücretlerini al
            var ucretler = _context.UzmanProfilleri
                .Where(up => up.AltKategoriId == altKategori.AltKategoriId)
                .Select(up => up.SaatlikUcret)
                .ToList();

            // Eğer hiç uzman yoksa, minimum ve maksimum ücretleri null yap
            if (ucretler.Count == 0)
            {
                model.MinSaatlikUcret = null;
                model.MaxSaatlikUcret = null;
            }
            else
            {
                model.MinSaatlikUcret = ucretler.Min();
                model.MaxSaatlikUcret = ucretler.Max();
            }

            return View(model);
        }



        [Route("Teklif/TeklifOlustur")]
        [HttpPost]
        public async Task<IActionResult> KategoriTeklifOlustur(KategoriTeklifViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return Json(new { success = false, message = "Hata oluştu.", errors = errorMessages });
            }

            var teklif = new Teklif
            {
                Baslik = model.Baslik,
                Aciklama = model.Aciklama,
                ZamanDamgasi = DateTime.Now,
                MusteriId = model.MusteriId,
                AltKategoriId = model.AltKategoriId,
                SehirId = model.SehirId,
                IlceId = model.IlceId,
                Durum = "Bekleniyor"
            };

            if (model.Fotograf != null && model.Fotograf.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");
                string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.Fotograf.FileName)}";
                var newPicturePath = Path.Combine(wwwrootFolder.First(x => x.Name == "img").PhysicalPath, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await model.Fotograf.CopyToAsync(stream);

                teklif.Fotograf = "/img/" + randomFileName;
            }

            _context.Teklifler.Add(teklif);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Teklif formunuz bize iletildi. Uzmanlarımız en kısa sürede size ulaşacaktır." });
        }




        [Route("Teklif/Uzman/{isim}")]
        public async Task<IActionResult> UzmanTeklif(string isim, int id)
        {
            isim = Uri.UnescapeDataString(isim).Replace("-", " ");

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "User"); // Kullanıcı oturum açmamışsa.
            }


            var musteri = _context.MusteriProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

            var uzman = _context.UzmanProfilleri.FirstOrDefault(u => (u.Kullanici.Ad + "_" + u.Kullanici.Soyad).Replace(" ", "_") == isim);

            if (uzman == null)
            {
                return NotFound(); // Girilen isimle eşleşen uzman bulunamadı.
            }

            var model = new UzmanTeklifViewModel
            {
                MusteriId = musteri.MusteriProfilId,
                UzmanId = uzman.UzmanProfilId,
                Tarih = DateTime.Now,
                AltKategoriId = uzman?.AltKategoriId,
                UzmanAdi = uzman?.Ad + " " + uzman?.Soyad,

            };

            return View(model);
        }



        [Route("Teklif/UzmanTeklifOlustur")]
        [HttpPost]
        public async Task<IActionResult> UzmanTeklifOlustur(UzmanTeklifViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return Json(new { success = false, message = "Hata oluştu.", errors = errorMessages });
            }

            if (model.AltKategoriId == null)
            {
                return Json(new { success = false, message = "Alt Kategori Id gereklidir." });
            }

            var teklif = new Teklif
            {
                Baslik = model.Baslik,
                Aciklama = model.Aciklama,
                ZamanDamgasi = DateTime.Now,
                MusteriId = model.MusteriId,
                UzmanProfilId = model.UzmanId,
                AltKategoriId = model.AltKategoriId,
                Durum = "Bekleniyor"
            };

            if (model.Fotograf != null && model.Fotograf.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");
                string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.Fotograf.FileName)}";
                var newPicturePath = Path.Combine(wwwrootFolder.First(x => x.Name == "img").PhysicalPath, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await model.Fotograf.CopyToAsync(stream);

                teklif.Fotograf = "/img/" + randomFileName;
            }

            _context.Teklifler.Add(teklif);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Teklif formunuz bize iletildi. Uzmanlarımız en kısa sürede size ulaşacaktır." });

        }


		
		public async Task<IActionResult>Detay(int id)
        {
           
            var teklifler = await _context.Teklifler.Include(k => k.AltKategori).Include(s => s.Sehir).Include(i => i.Ilce)
                .FirstOrDefaultAsync(t => t.TeklifId == id);

            return View(teklifler);
        }


    }
}
