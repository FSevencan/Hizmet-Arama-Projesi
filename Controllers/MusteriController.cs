using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.ViewModels;
using System.Data;

namespace Proje.Controllers
{
    public class MusteriController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MusteriController(AppDbContext context, IFileProvider fileProvider, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _fileProvider = fileProvider;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [Authorize(Roles = "Müşteri")]
        public async Task<IActionResult> CustomerProfileEdit()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentProfile = _context.MusteriProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

            var userEditViewModel = new CustomerProfileEditViewModel()
            {
                Ad = currentUser.Ad,
                Soyad = currentUser.Soyad,
                Email = currentUser.Email,
                Telefon = currentProfile?.Telefon,
                SehirId = currentProfile?.SehirId,
                IlceId = currentProfile?.IlceId,
                PostaKodu = currentProfile?.PostaKodu,
                Adres = currentProfile?.Adres,
                PictureShow = currentUser.Picture,

                Sehirler = _context.Sehirler.ToList(),
                Ilceler = currentProfile != null ? _context.Ilceler.Where(ilce => ilce.SehirId == currentProfile.SehirId).ToList() : new List<Ilce>()
            };

            return View(userEditViewModel);
        }

        [Authorize(Roles = "Müşteri")]
        [HttpPost]
        public async Task<IActionResult> CustomerProfileEdit(CustomerProfileEditViewModel model)
        {
            model.Sehirler = _context.Sehirler.ToList();
            model.Ilceler = _context.Ilceler.Where(ilce => ilce.SehirId == model.SehirId).ToList();

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var currentProfile = _context.MusteriProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);

                currentUser.Ad = model.Ad;
                currentUser.Soyad = model.Soyad;
                currentUser.Email = model.Email;

                if (currentProfile != null)
                {
                    currentProfile.Telefon = model.Telefon;
                    currentProfile.SehirId = model.SehirId;
                    currentProfile.IlceId = model.IlceId;
                    currentProfile.PostaKodu = model.PostaKodu;
                    currentProfile.Adres = model.Adres;


                    _context.MusteriProfilleri.Update(currentProfile);
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


        public async Task<IActionResult> Tekliflerim()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var musteri = _context.MusteriProfilleri.SingleOrDefault(p => p.KullaniciId == currentUser.Id);


            var teklifler = _context.Teklifler
                .Include(t => t.Musteri)
                .Include(t => t.AltKategori)
                .Include(t => t.Sehir)
                .Include(t => t.Ilce)
                .Include(t => t.Mesajlar)
				.Where(t => t.Musteri.KullaniciId == currentUser.Id)
				.OrderByDescending(t => t.ZamanDamgasi)
                .ToList();

            var viewModel = new TekliflerViewModel
            {
                Teklifler = teklifler,
                CurrentUserId = currentUser.Id,
            };

            return View(viewModel);
        }


		[HttpPost]
		public async Task<IActionResult> TeklifSil(int id)
		{
			var teklif = await _context.Teklifler.FindAsync(id);

			if (teklif == null)
			{
				return NotFound();
			}

			_context.Teklifler.Remove(teklif);
			await _context.SaveChangesAsync();

			return RedirectToAction("Tekliflerim");
		}



	}
}
