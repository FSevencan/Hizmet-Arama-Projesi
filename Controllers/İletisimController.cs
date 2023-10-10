using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
    public class İletisimController : Controller
    {
        private readonly AppDbContext _context;

        public İletisimController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index( İletisim İletisim)
        {
            if (ModelState.IsValid)
            {
                İletisim.GonderilmeTarihi = DateTime.Now;
                _context.Add(İletisim);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Teşekkürler, mesajınız bize iletildi." });
            }

			else
            {
                return Json(new { success = false, message = "Form hatalı, lütfen tekrar deneyin." });
            }
        }
    }
}
