using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Proje.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminİletisimController : Controller
    {
        private readonly AppDbContext _context;

        public AdminİletisimController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
              return _context.Mesajlar != null ? 
                          View(await _context.İletisimMesajları.OrderByDescending(m=> m.GonderilmeTarihi).ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Mesajlar'  is null.");
        }


		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound(new { message = "Geçersiz ID." });
			}

			var iletisim = await _context.İletisimMesajları
				.FirstOrDefaultAsync(m => m.Id == id);

			if (iletisim == null)
			{
				return NotFound(new { message = "İletişim mesajı bulunamadı." });
			}

			iletisim.OkunmaTarihi = DateTime.Now;
			_context.SaveChanges();

			return Json(new
			{
				Mesaj = iletisim.Mesaj,
				AdSoyad = iletisim.AdSoyad,
			});
		}



		[HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mesajlar == null)
            {
                return Problem("Entity set 'AppDbContext.Mesajlar'  is null.");
            }
            var İletisim = await _context.İletisimMesajları.FindAsync(id);
            if (İletisim != null)
            {
                _context.İletisimMesajları.Remove(İletisim);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        private bool İletisimExists(int id)
        {
          return (_context.İletisimMesajları?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
