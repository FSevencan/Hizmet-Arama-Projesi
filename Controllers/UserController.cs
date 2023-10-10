using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Proje.Extenisons;
using Proje.Models;
using Proje.Services;
using Proje.ViewModels;
using System.Security.Claims;

namespace Proje.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        public UserController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, AppDbContext context)
        {
            _logger = logger;
            _UserManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
        }
        public IActionResult CustomerSignUp()
        {
            return View();
        }

        public IActionResult ExpertSignUp()
        {
            return View();
        }
        public IActionResult SignIn()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl ??= Url.Action("Index", "Home");

            var hasUser = await _UserManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz email veya şifre.");
                return View();
            }

            // Kullanıcının hesabının doğrulanıp doğrulanmadığını kontrol et
            if (!hasUser.IsVerified) // hasUser.IsVerified özelliği doğrudan kullanıldı
            {
                ModelState.AddModelError(string.Empty, "Lütfen e-posta adresinizi doğrulayın.");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriş yapamazsınız." });
                return View();
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Geçersiz email veya şifre", $"Başarısız giriş sayısı = {await _UserManager.GetAccessFailedCountAsync(hasUser)}" });
                return View();
            }

            return Redirect(returnUrl!);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerSignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errors });
            }

            var user = new AppUser
            {
                Ad = request.FirstName,
                Soyad = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                Picture = "pp.png",
                IsVerified = false,
            };


            var identityResult = await _UserManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }

            // Kullanıcıya Musteri rolünü atıyoruz
            var addToRoleResult = await _UserManager.AddToRoleAsync(user, "Müşteri");
            if (!addToRoleResult.Succeeded)
            {
                var errors = addToRoleResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }


            var musteriProfil = new MusteriProfil
            {
                KullaniciId = user.Id,
                Ad = request.FirstName,
                Soyad = request.LastName,
                Email = request.Email
            };

            _context.MusteriProfilleri.Add(musteriProfil);
            await _context.SaveChangesAsync();

            var exchangeExpireClaim = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString());

            var userFromDb = await _UserManager.FindByNameAsync(request.Email);

            var claimResult = await _UserManager.AddClaimAsync(userFromDb!, exchangeExpireClaim);

            if (!claimResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }

            // Kullanıcı oluşturma işlemi bittikten sonra doğrulama kodunu oluştur
            string verificationCode = _emailService.GenerateVerificationCode();

            // VerificationCode ve VerificationCodeExpiration değerlerini kullanıcıya ayarla
            userFromDb.VerificationCode = verificationCode;
            userFromDb.VerificationCodeExpiration = DateTime.Now.AddHours(1);

            // Kullanıcıyı güncelle
            await _UserManager.UpdateAsync(userFromDb);

            // Kullanıcıya doğrulama kodunu arka planda gönder
            _ = Task.Run(async () =>
            {
                await _emailService.SendVerificationEmail(verificationCode, userFromDb.Email);
            });

            return Json(new { success = true }); // action sonunda JSON yanıtı döndürüldü
        }


        [HttpPost]
        public async Task<IActionResult> ExpertSignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errors });
            }

            var user = new AppUser
            {
                Ad = request.FirstName,
                Soyad = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                Picture = "pp.png",
                IsVerified = false,
            };


            var identityResult = await _UserManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }

            // Kullanıcıya Uzman rolünü atıyoruz
            var addToRoleResult = await _UserManager.AddToRoleAsync(user, "Uzman");
            if (!addToRoleResult.Succeeded)
            {
                var errors = addToRoleResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }

            var uzmanProfil = new UzmanProfil
            {
                KullaniciId = user.Id,
                Ad = request.FirstName,
                Soyad = request.LastName,
                Email = request.Email,
                KayıtTarihi = DateTime.Now,
            };

            _context.UzmanProfilleri.Add(uzmanProfil);
            await _context.SaveChangesAsync();

            var exchangeExpireClaim = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString());

            var userFromDb = await _UserManager.FindByNameAsync(request.Email);

            var claimResult = await _UserManager.AddClaimAsync(userFromDb!, exchangeExpireClaim);

            if (!claimResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToList();
                return Json(new { errors });
            }

            // Kullanıcı oluşturma işlemi bittikten sonra doğrulama kodunu oluştur
            string verificationCode = _emailService.GenerateVerificationCode();

            // VerificationCode ve VerificationCodeExpiration değerlerini kullanıcıya ayarla
            userFromDb.VerificationCode = verificationCode;
            userFromDb.VerificationCodeExpiration = DateTime.Now.AddHours(1);

            // Kullanıcıyı güncelle
            await _UserManager.UpdateAsync(userFromDb);

            // Kullanıcıya doğrulama kodunu arka planda gönder
            _ = Task.Run(async () =>
            {
                await _emailService.SendVerificationEmail(verificationCode, userFromDb.Email);
            });

            return Json(new { success = true }); // action sonunda JSON yanıtı döndürüldü
        }


        [HttpPost]
        public async Task<IActionResult> VerifyCode(string email, string code)
        {
            var user = await _UserManager.FindByEmailAsync(email);



            if (user.VerificationCode != code)
            {
                return Json(new { success = false, message = "Geçersiz doğrulama kodu." });
            }

            if (user.VerificationCodeExpiration < DateTime.Now)
            {
                return Json(new { success = false, message = "Doğrulama kodunun süresi doldu." });
            }

            user.IsVerified = true; // hesap doğrulandı
            await _UserManager.UpdateAsync(user);

            return Json(new { success = true, message = "Hesabınız başarıyla doğrulandı. Şimdi giriş yapabilirsiniz." });
        }


        [HttpPost]
        public async Task<IActionResult> ResendCode(string email)
        {
            // E-posta adresini kullanarak kullanıcıyı bul
            var userFromDb = await _UserManager.FindByEmailAsync(email);

            if (userFromDb != null)
            {
                // Yeni bir doğrulama kodu oluştur
                string newVerificationCode = _emailService.GenerateVerificationCode();

                // Kullanıcının doğrulama kodunu ve son kullanma tarihini güncelle
                userFromDb.VerificationCode = newVerificationCode;
                userFromDb.VerificationCodeExpiration = DateTime.Now.AddHours(1);

                // Kullanıcı bilgilerini güncelle
                var result = await _UserManager.UpdateAsync(userFromDb);

                if (result.Succeeded)
                {
                    // Kullanıcıya yeni doğrulama kodunu arka planda gönder
                    _ = Task.Run(async () =>
                    {
                        await _emailService.SendVerificationEmail(newVerificationCode, userFromDb.Email);
                    });

                    // Başarı durumunu gönder
                    return Ok(new { success = true });
                }
            }

            // Kullanıcı bulunamazsa veya güncelleme başarısız olursa, hata durumunu gönder
            return BadRequest(new { success = false });
        }


		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
		{
			var hasUser = await _UserManager.FindByEmailAsync(request.Email);

			if (hasUser == null)
			{
				ModelState.AddModelError(String.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır.");
				return View();
			}

			string passwordResestToken = await _UserManager.GeneratePasswordResetTokenAsync(hasUser);

			var passwordResetLink = Url.Action("ResetPassword", "User", new { userId = hasUser.Id, Token = passwordResestToken }, HttpContext.Request.Scheme);


			await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);

			TempData["SuccessMessage"] = "Şifre yenileme linki, e-posta adresinize gönderilmiştir.";

			return RedirectToAction(nameof(ForgetPassword));
		}

		public IActionResult ResetPassword(string userId, string token)
		{
			TempData["userId"] = userId;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
		{
			var userId = TempData.Peek("userId");
			var token = TempData.Peek("token");

			if (userId == null || token == null)
			{
				throw new Exception("Bir hata meydana geldi");
			}

			var hasUser = await _UserManager.FindByIdAsync(userId.ToString()!);

			if (hasUser == null)
			{
				return Json(new { success = false, message = "Kullanıcı bulunamamıştır." });
			}

			if (request.Password != request.PasswordConfirm)
			{
				return Json(new { success = false, message = "Şifreler aynı değil." });
			}

			if (string.IsNullOrEmpty(request.Password))
			{
				return Json(new { success = false, message = "Şifre alanı boş bırakılamaz." });
			}

			IdentityResult result = await _UserManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);

			if (result.Succeeded)
			{
				return Json(new { success = true, message = "Şifreniz başarıyla yenilenmiştir." });
			}
			else
			{
				List<string> errors = result.Errors.Select(x => x.Description).ToList();
				return Json(new { success = false, errors = errors });
			}
		}





	}
}
