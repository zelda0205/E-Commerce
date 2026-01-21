using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ZELDA.Data;
using ZELDA.Models;
using ZELDA.ViewModels;

namespace ZELDA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(GetProductsAndCategories());
        }

        public IActionResult Shop()
        {
            return View(GetProductsAndCategories());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Clothes()
        {
            return View();
        }

        public IActionResult Accessories()
        {
            return View();
        }

        public IActionResult Bags()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private ProductAndCategoryViewModel GetProductsAndCategories()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            return new ProductAndCategoryViewModel
            {
                Products = products,
                Categories = categories
            };

        }
    }
}
