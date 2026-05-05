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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // 1. READ-ONLY PROFILE DASHBOARD
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

        // 2. GET: EDIT PERSONAL INFO FORM
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email
            };

            return View(model);
        }

        // POST: EDIT PERSONAL INFO FORM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            // Remove OrderHistory validation check since it's not present on this form
            ModelState.Remove(nameof(model.OrderHistory));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Të dhënat tuaja u përditësuan me sukses!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // 3. GET: CHANGE PASSWORD FORM
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        // POST: CHANGE PASSWORD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Fjalëkalimi u ndryshua me sukses!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // 4. POST: DELETE ACCOUNT
        // 1. GET: Shfaq faqen e konfirmimit të fshirjes me të dhënat e përdoruesit
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            return View(model);
        }

        // 2. POST: Ekzekuton fshirjen përfundimtare pas konfirmimit
        [HttpPost]
        [ActionName("DeleteAccountConfirmed")] // Ndryshojmë emrin për siguri
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Çaktivizojmë seancën (Sign Out)
            await _signInManager.SignOutAsync();

            // Fshijmë përdoruesin nga databaza e Identity
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Ju ridrejton tek faqja e regjistrimit ose login-it
                return RedirectToAction("Register", "Account");
            }

            // Nëse dështon për ndonjë arsye, e kyçim sërish dhe njoftojmë
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["ErrorMessage"] = "Something went wrong while trying to delete your account.";
            return RedirectToAction(nameof(Index));
        }
    }
    
}