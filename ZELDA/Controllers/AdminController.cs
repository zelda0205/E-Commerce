using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ZELDA.Data;
using ZELDA.Models;

namespace ZELDA.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            var model = new ViewModels.AdminDashboardViewModel
            {
                TotalNumberOfUsers = _userManager.Users.Count(),
                TotalNumberOfOrders = _context.Orders.Count(),
                TotalNumberFeedbacks = _context.ContactUs.Count()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UnblockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Users));
        }

    }
}