using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.ViewModels;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Proje.Controllers
{
    public class UzmanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UzmanController(AppDbContext context, IFileProvider fileProvider, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _fileProvider = fileProvider;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [Route("Uzman/{isim}")]
        public async Task<IActionResult> Index(string isim)
        {
            var uzmanProfil = await _context.UzmanProfilleri
                .Include(u => u.Kategori)
                .Include(u => u.AltKategori)
                .Include(u => u.Sehir)
                .Include(u => u.Ilce)
                .Include(u => u.Kullanici)
                .FirstOrDefaultAsync(u => (u.Kullanici.Ad + "_" + u.Kullanici.Soyad).Replace(" ", "_") == isim);

            if (uzmanProfil == null)
            {
                return NotFound();
            }

            var uzmanYorumlar = await _context.UzmanYorumlari
                .Include(y => y.Musteri)
                .ThenInclude(m => m.Kullanici)
                .Where(y => y.UzmanProfilId == uzmanProfil.UzmanProfilId).OrderByDescending(y => y.YorumTarihi)
                .ToListAsync();

            var yildizDagilimi = uzmanYorumlar
                .GroupBy(y => y.Derece)
                .Where(g => g.Key.HasValue)
                .ToDictionary(g => g.Key.Value, g => g.Count());

            var viewModel = new Uzman_YorumViewModel
            {
                UzmanProfil = uzmanProfil,
                UzmanYorumlar = uzmanYorumlar,
                YildizDagilimi = yildizDagilimi
            };

            ViewData["UzmanId"] = uzmanProfil.UzmanProfilId;

            return View(viewModel);
        }


        [Route("Uzman/ExpertProfileEdit")]
        [Authorize(Roles = "Uzman")]
        public async Task<IActionResult> ExpertProfileEdit()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentProfile = _context.UzmanProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

            var userEditViewModel = new ExpertProfileEditViewModel()
            {
                Ad = currentUser.Ad,
                Soyad = currentUser.Soyad,
                Email = currentUser.Email,
                Telefon = currentProfile?.Telefon,
                SehirId = currentProfile?.SehirId,
                IlceId = currentProfile?.IlceId,
                Title = currentProfile?.Title,
                Aciklama = currentProfile?.Aciklama,
                SaatlikUcret = currentProfile?.SaatlikUcret,
                PictureShow = currentUser.Picture,
                KategoriId = currentProfile?.KategoriId,
                AltKategoriId = currentProfile?.AltKategoriId,

                Sehirler = _context.Sehirler.ToList(),
                Ilceler = currentProfile != null ? _context.Ilceler.Where(ilce => ilce.SehirId == currentProfile.SehirId).ToList() : new List<Ilce>(),
                Kategoriler = _context.Kategoriler.ToList(),
                AltKategoriler = _context.AltKategoriler.ToList(),
            };

            return View(userEditViewModel);
        }

        [Authorize(Roles = "Uzman")]
        [Route("Uzman/ExpertProfileEdit")]
        [HttpPost]
        public async Task<IActionResult> ExpertProfileEdit(ExpertProfileEditViewModel model)
        {
            model.Sehirler = _context.Sehirler.ToList();
            model.Ilceler = _context.Ilceler.Where(ilce => ilce.SehirId == model.SehirId).ToList();
            model.Kategoriler = _context.Kategoriler.ToList();
            model.AltKategoriler = _context.AltKategoriler.ToList();

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var currentProfile = _context.UzmanProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

                currentUser.Ad = model.Ad;
                currentUser.Soyad = model.Soyad;
                currentUser.Email = model.Email;

                if (currentProfile != null)
                {
                    currentProfile.Telefon = model.Telefon;
                    currentProfile.SehirId = model.SehirId;
                    currentProfile.IlceId = model.IlceId;
                    currentProfile.Title = model.Title;
                    currentProfile.Aciklama = model.Aciklama;
                    currentProfile.SaatlikUcret = model.SaatlikUcret;
                    currentProfile.KategoriId = model.KategoriId;
                    currentProfile.AltKategoriId = model.AltKategoriId;

                    _context.UzmanProfilleri.Update(currentProfile);
                }

                if (model.Picture != null && model.Picture.Length > 0)
                {
                    var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");

                    string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.Picture.FileName)}";

                    var newPicturePath = Path.Combine(wwwrootFolder!.First(x => x.Name == "img").PhysicalPath!, randomFileName);

                    using var stream = new FileStream(newPicturePath, FileMode.Create);

                    await model.Picture.CopyToAsync(stream);

                    currentUser.Picture = randomFileName;
                }

                var result = await _userManager.UpdateAsync(currentUser);

                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Profil bilgileriniz başarılı bir şekilde güncellenmiştir." });
                }
                else
                {
                    var errorMessages = string.Join("\n", result.Errors.Select(error => error.Description));
                    return Json(new { success = false, message = "Profil bilgilerinizi güncellerken bir hata oluştu.", errors = errorMessages });
                }
            }

            return View(model);
        }


        [Route("Uzman/BanaUygunIsler/{sayfaNumarasi=1}")]
        public async Task<IActionResult> BanaUygunIsler(int sayfaNumarasi, int sayfaBoyutu = 6)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("SignIn", "User"); // Kullanıcı oturum açmamışsa.
            }

            var uzman = _context.UzmanProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

            if (uzman == null)
            {
                return NotFound(); // Girilen isimle eşleşen uzman bulunamadı.
            }

            // Toplam sayfa sayısını hesapla
            int toplamTeklifSayisi = _context.Teklifler.Where(t => t.AltKategoriId == uzman.AltKategoriId &&
                                                t.SehirId == uzman.SehirId &&
                                                t.IlceId == uzman.IlceId).Count();
            int toplamSayfaSayisi = (int)Math.Ceiling(toplamTeklifSayisi / (double)sayfaBoyutu);

            // Teklifleri sayfala
            int skip = (sayfaNumarasi - 1) * sayfaBoyutu;

            var teklifler = _context.Teklifler.Include(u => u.AltKategori).Include(u=> u.Musteri)
                .Include(u => u.Sehir)
                .Include(u => u.Ilce).OrderByDescending(u=> u.ZamanDamgasi)
                .Where(t => t.AltKategoriId == uzman.AltKategoriId &&
                                                t.SehirId == uzman.SehirId &&
                                                t.IlceId == uzman.IlceId)
                .Skip(skip)
                .Take(sayfaBoyutu)
                .ToList();

            // Toplam sayfa sayısını view'a aktar
            ViewBag.ToplamSayfaSayisi = toplamSayfaSayisi;
            ViewBag.SayfaNo = sayfaNumarasi;

            return View(teklifler);
        }

		// Profilden Özel Olarak Gönderilen Teklifler
		[Route("Uzman/Tekliflerim")]
		public async Task<IActionResult> Tekliflerim()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var uzman = _context.UzmanProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);


            var teklifler = _context.Teklifler
                .Include(t => t.Musteri)
                .Include(t => t.AltKategori)
                .Include(t => t.Sehir)
                .Include(t => t.Ilce)
                .Include(t => t.Mesajlar)
                .Where(t => t.UzmanProfilId == uzman.UzmanProfilId)
                .OrderByDescending(t => t.ZamanDamgasi)
                .ToList();

            var viewModel = new TekliflerViewModel
            {
                Teklifler = teklifler,
                CurrentUserId = currentUser.Id,
            };

            return View(viewModel);
        }
    }
}
