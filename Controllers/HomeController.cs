using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using System.Diagnostics;

namespace Proje.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _context;
		CokluListeleme _ckl = new CokluListeleme();

		public HomeController(AppDbContext context)
		{
			_context = context;
		}


		public IActionResult Index()
		{
			_ckl.Kategoriler = _context.Kategoriler.ToList();

			ViewData["PopulerKategoriler"] = _context.AltKategoriler
			.Include(k => k.Kategori)
			.OrderBy(c => Guid.NewGuid())
			.Take(4)
			.ToList();

			ViewData["PopulerUzmanlar"] = _context.UzmanProfilleri
            .Include(u => u.Kategori)
            .Include(u => u.AltKategori)
            .Include(u => u.Sehir)
            .Include(u => u.Ilce)
            .Include(u => u.Kullanici)
            .Include(u => u.UzmanYorumlari)
            .OrderBy(c => Guid.NewGuid())
            .Take(3)
            .Select(u => new
            {
	           Uzman = u,
				// Ortalama puanı yorumların Derece alanından hesapla
				OrtalamaPuan = u.UzmanYorumlari.Any() ? u.UzmanYorumlari.Average(y => y.Derece.HasValue ? y.Derece.Value : 0) : 0, 

	           YorumSayisi = u.UzmanYorumlari.Count
            })
           .ToList();

			return View(_ckl);
		}


	}
}