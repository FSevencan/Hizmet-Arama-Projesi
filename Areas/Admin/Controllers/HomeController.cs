using Proje.Areas.Admin.Models;
using Proje.Models;
using Proje.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proje.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;
		CokluListeleme _ckl = new CokluListeleme();

		public HomeController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }

      
        public IActionResult Index()
        {
            _ckl.AltKategoriler= _context.AltKategoriler.ToList();
            _ckl.MusteriProfilleri= _context.MusteriProfilleri.ToList();
            _ckl.UzmanProfilleri= _context.UzmanProfilleri.ToList();
            _ckl.Teklifler= _context.Teklifler.ToList();

			return View(_ckl);
        }

       
        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.OrderByDescending(u=> u.Id).ToListAsync();

            var userViewModelList = userList.Select(x => new Models.UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.Ad,
                LastName = x.Soyad,
                Telefon = x.PhoneNumber,
                Fotograf = x.Picture

            }).ToList();

            return View(userViewModelList);
        }

        
        [HttpPost]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);

				if (result.Succeeded)
				{
					return Json(new { success = true });
				}
				
			}

			return Json(new { success = false });
		}


	}
}