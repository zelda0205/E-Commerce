using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ZELDA.Data;
using ZELDA.Models;

namespace ZELDA.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactUsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ContactUs model = new();

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var loggedInUser = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (loggedInUser != null)
                {
                    model.Name = loggedInUser.FirstName + " " + loggedInUser.LastName;
                    model.Email = loggedInUser.Email!;
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return _context.ContactUs != null ?
                        View(await _context.ContactUs.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var contactUs = _context.ContactUs.Find(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            _context.ContactUs.Remove(contactUs);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Execute(ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }


            _context.ContactUs.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}