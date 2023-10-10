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

	public class AdminAboneController : Controller
    {
        private readonly AppDbContext _context;

        public AdminAboneController(AppDbContext context)
        {
            _context = context;
        }

       
		public async Task<IActionResult> Index()
		{
			if (_context.Aboneler != null)
			{
				var Aboneler = await _context.Aboneler.OrderByDescending(a => a.AboneId).ToListAsync();
				return View(Aboneler);
			}
			else
			{
				return Problem("Entity set 'AppDbContext.Abone' is null.");
			}
		}

		
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aboneler == null)
            {
                return Problem("Entity set 'AppDbContext.Abone'  is null.");
            }
            var abone = await _context.Aboneler.FindAsync(id);
            if (abone != null)
            {
                _context.Aboneler.Remove(abone);
            }
            
            await _context.SaveChangesAsync();
			return Json(new { success = true });
		}

        private bool AboneExists(int id)
        {
          return (_context.Aboneler?.Any(e => e.AboneId == id)).GetValueOrDefault();
        }
    }
}
