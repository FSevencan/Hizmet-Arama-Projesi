
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace Proje.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;
        private readonly AppDbContext _context;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
            _context = context;
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


        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors = errors });
            }

            var currentUser = (await _userManager.FindByNameAsync(User.Identity!.Name!))!;

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkOldPassword)
            {
                return BadRequest(new { success = false, message = "Eski şifreniz yanlış" });
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.PasswordNew);

            if (!resultChangePassword.Succeeded)
            {
                var errors = resultChangePassword.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { success = false, errors = errors });
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.PasswordNew, true, false);

            // SweetAlert için başarı mesajını JSON olarak döndürüyoruz
            return Json(new { success = true, message = "Şifreniz başarıyla değiştirilmiştir." });
        }


        public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult GetIlceler(int sehirId)
		{
			var ilceler = _context.Ilceler.Where(i => i.SehirId == sehirId).Select(i => new { IlceId = i.IlceId, IlceAdi = i.IlceAdi }).ToList();
			return Json(ilceler);
		}

        [HttpGet]
        public async Task<IActionResult> GetAltKategoriler(int kategoriId)
        {
            var altKategoriler = await _context.AltKategoriler
                .Where(ak => ak.KategoriId == kategoriId)
                .Select(ak => new { altKategoriId = ak.AltKategoriId, altKategoriAdi = ak.Isim })
                .ToListAsync();

            return Json(altKategoriler);
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            string message = string.Empty;

            message = "Bu sayfayı görmeye yetkiniz yoktur. Yetki almak için  yöneticiniz ile görüşebilirsiniz.";

            ViewBag.message = message;
            return View();
        }

        


    }
}
