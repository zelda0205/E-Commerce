using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZELDA.Data;
using ZELDA.Models;

namespace ZELDA.Controllers
{
    public class NewsLettersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsLettersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsLetters.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email")] NewsLetter newsLetter)
        {
            if (NewsLetterExists(newsLetter.Email!))
            {
                return Json(null);
            }

            newsLetter.SubscribedAt = DateTime.Now;
            _context.Add(newsLetter);
            await _context.SaveChangesAsync();

            return Json(null);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetter = await _context.NewsLetters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLetter == null)
            {
                return NotFound();
            }

            return View(newsLetter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsLetter = await _context.NewsLetters.FindAsync(id);
            if (newsLetter != null)
            {
                _context.NewsLetters.Remove(newsLetter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsLetterExists(string email)
        {
            return _context.NewsLetters.Any(e => e.Email == email);
        }
    }
}