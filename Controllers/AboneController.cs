using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
	public class AboneController : Controller
	{
		private readonly AppDbContext _context;
		

		public AboneController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Abone(string email)
		{
			if (!IsValidEmail(email))
			{
				TempData["ErrorMessage"] = "Lütfen geçerli bir email adresi girin.";
				return Json(new { success = false, message = "Lütfen geçerli bir email adresi girin." });
			}

			var existingSubscriber = _context.Aboneler.FirstOrDefault(s => s.Email == email);

			if (existingSubscriber != null)
			{
				TempData["ErrorMessage"] = "Bu e-posta adresi zaten kayıtlı.";
				return Json(new { success = false, message = "Bu e-posta adresi zaten kayıtlı." });
			}

			var subscriber = new Abone
			{
				Email = email
			};

			_context.Aboneler.Add(subscriber);
			_context.SaveChanges();
			TempData["SuccessMessage"] = "Başarıyla abone oldunuz!";

			return Json(new { success = true });
		}

		private bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}
