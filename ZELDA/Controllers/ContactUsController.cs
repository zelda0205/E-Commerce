using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZELDA.Data;

namespace ZELDA.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
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
        public JsonResult Delete(int? id)
        {

            if (id == null)
                return new JsonResult(BadRequest());

            var contactUs = _context.ContactUs.Find(id);

            if (contactUs == null)
                return new JsonResult(NotFound());

            _context.ContactUs.Remove(contactUs);
            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Execute(ZELDA.Models.ContactUs model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home");

            _context.ContactUs.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private bool ContactUsExists(int id)
        {
            return (_context.ContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
