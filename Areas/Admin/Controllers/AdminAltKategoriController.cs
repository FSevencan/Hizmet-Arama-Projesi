using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Proje.Models;

namespace Proje.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminAltKategoriController : Controller
    {
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public AdminAltKategoriController(AppDbContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}


		public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AltKategoriler.Include(a => a.Kategori);
            return View(await appDbContext.ToListAsync());
        }

    
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "Isim");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AltKategoriId,Isim,Fotograf,Aciklama,KategoriId")] AltKategori altKategori , IFormFile Fotograf)
        {
            if (ModelState.IsValid)
            {

                var photoName = Guid.NewGuid().ToString()
              + new Random().Next(0, 1000)
              + System.IO.Path.GetExtension(Fotograf.FileName);

                using (var stream = new FileStream(
                           Path.Combine(_environment.WebRootPath, "img/", photoName),
                           FileMode.Create))
                {
                    await Fotograf.CopyToAsync(stream);

                    altKategori.Fotograf = photoName;
                }

                _context.Add(altKategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


			if (!ModelState.IsValid)
			{
				foreach (var modelStateKey in ModelState.Keys)
				{
					var modelStateVal = ModelState[modelStateKey];
					foreach (var error in modelStateVal.Errors)
					{
						// her bir hata için bu blok çalışacak ve hata detaylarını çıktı olarak verecektir.
						Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
					}
				}
			}


			ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "Isim", altKategori.KategoriId);
            return View(altKategori);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AltKategoriler == null)
            {
                return NotFound();
            }

            var altKategori = await _context.AltKategoriler.FindAsync(id);
            if (altKategori == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "Isim", altKategori.KategoriId);
            return View(altKategori);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AltKategoriId,Isim,Fotograf,Aciklama,KategoriId")] AltKategori altKategori, IFormFile? file)
        {
            if (id != altKategori.AltKategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

					if (file != null)
					{

						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


						using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath, "img/", fileName), FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						string silinecek = Path.Combine(_environment.WebRootPath, "img/", altKategori.Fotograf);
						System.IO.File.Delete(silinecek);


						altKategori.Fotograf = fileName;
					}



					_context.Update(altKategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltKategoriExists(altKategori.AltKategoriId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "Isim", altKategori.KategoriId);
            return View(altKategori);
        }


		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.AltKategoriler == null)
			{
				return Problem("Entity set 'AppDbContext.Categories'  is null.");
			}
			var altKategori = await _context.AltKategoriler.FindAsync(id);
			if (altKategori != null)
			{
				_context.AltKategoriler.Remove(altKategori);
			}

			await _context.SaveChangesAsync();

			return Json(new { success = true });
		}

		private bool AltKategoriExists(int id)
        {
          return (_context.AltKategoriler?.Any(e => e.AltKategoriId == id)).GetValueOrDefault();
        }
    }
}
