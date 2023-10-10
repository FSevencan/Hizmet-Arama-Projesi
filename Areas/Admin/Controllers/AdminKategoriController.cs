
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using System.Data;

namespace Proje.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminKategoriController : Controller
    {
        private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public AdminKategoriController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
			_environment = environment;
		}


        public async Task<IActionResult> Index()
        {
              return _context.Kategoriler != null ? 
                          View(await _context.Kategoriler.OrderByDescending(k=> k.KategoriId).ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Kategoriler'  is null.");
        }

       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,Isim,Fotograf")] Kategori kategori, IFormFile Fotograf)
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

                    kategori.Fotograf = photoName;
                }

                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


			return View(kategori);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,Isim,Fotograf")] Kategori kategori, IFormFile? file)
        {
            if (id != kategori.KategoriId)
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

						string silinecek = Path.Combine(_environment.WebRootPath, "img/", kategori.Fotograf);
						System.IO.File.Delete(silinecek);


						kategori.Fotograf = fileName;
					}

					_context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.KategoriId))
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
            return View(kategori);
        }



		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Kategoriler == null)
			{
				return Problem("Entity set 'AppDbContext.Categories'  is null.");
			}
			var kategori = await _context.Kategoriler.FindAsync(id);
			if (kategori != null)
			{
				_context.Kategoriler.Remove(kategori);
			}

			await _context.SaveChangesAsync();

			return Json(new { success = true });
		}

		private bool KategoriExists(int id)
        {
          return (_context.Kategoriler?.Any(e => e.KategoriId == id)).GetValueOrDefault();
        }
    }
}
