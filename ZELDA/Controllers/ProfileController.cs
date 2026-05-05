using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZELDA.Data; 
using ZELDA.Models;
using ZELDA.ViewModels;

namespace ZELDA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var orders = await _context.Orders
                .Where(o => o.UserId == user.Id) 
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            
            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                OrderHistory = orders
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
             
                var userRefresh = await _userManager.GetUserAsync(User);
                model.OrderHistory = await _context.Orders
                    .Where(o => o.UserId == userRefresh.Id)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

           
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profili u përditësua me sukses!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", model);
        }
    }
}
