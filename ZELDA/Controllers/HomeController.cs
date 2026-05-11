using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using ZELDA.Data;
using ZELDA.Filters;
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

        public async Task<IActionResult> Index()
        {
            var viewModel = await GetProductsAndCategories(false);
            return View(viewModel); 
        }

        [HttpGet]
        [MostSoldFilter]
        public async Task<IActionResult> GetTrendingProducts()
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (HttpContext.Items["IsMostSold"] is bool isMostSold && isMostSold)
            {
                query = query.OrderByDescending(p => p.OrderItems.Sum(oi => oi.Quantity));
            }
            else
            {
                query = query.OrderByDescending(p => p.CreatedAt);
            }

            var products = await query.Take(8).ToListAsync();
            return PartialView("_ProductListPartial", products);
        }

        public async Task<IActionResult> Shop()
        {
            return View(await GetProductsAndCategories());
        }

        public IActionResult About()
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

        [HttpPost]
        public async Task<IActionResult> FilterProducts(
              int? category,
              string? price,
              string? name)
        {
            var selectedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == category);

            string categoryName = string.Empty;

            if (selectedCategory != null)
            {
                categoryName = selectedCategory.Name;
            }
            else
            {
                categoryName = "All";
            }

            var products = await _context.Products.Include(p => p.Category)
                .ToListAsync();

            if (category.HasValue && category.Value > 0)
            {
                products = products.Where(p => p.CategoryID == category.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            if (price == "High")
            {
                products = products.OrderByDescending(p => p.Price).ToList();
            }
            else if (price == "Low")
            {
                products = products.OrderBy(p => p.Price).ToList();
            }

            var viewModel = new ProductAndCategoryViewModel
            {
                Products = products,
                Categories = _context.Categories,
                SelectedCategory = categoryName,
            };

            return View(viewModel);
        }

        private async Task<ProductAndCategoryViewModel> GetProductsAndCategories(bool sortByMostSold = false)
        {

            IQueryable<Product> productQuery = _context.Products;

            if (sortByMostSold)
            {
                productQuery = productQuery.OrderByDescending(p => p.OrderItems.Sum(oi => oi.Quantity));
            }
            else
            {
                productQuery = productQuery.OrderByDescending(p => p.CreatedAt);
            }

            return new ProductAndCategoryViewModel
            {
                Products = await productQuery.Take(8).ToListAsync(), 
                Categories = await _context.Categories.ToListAsync()
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}