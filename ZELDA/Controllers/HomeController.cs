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
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = new List<Product>
    {
        new Product
        {
            ProductID = 1,
            Name = "Beige and Red Crochet Dress",
            Description = "A beautiful beige and red crochet dress perfect for summer outings.",
            Price = 95.00m,
            ProductImages = new List<ProductImage>
            {
                new ProductImage
                {
                    ImageUrl = "/foto/clothes/f1.jpg"
                }
            }
        }
    };
            return View(products); ;
        }

        public IActionResult Shop(int id=1)
        {
            var product = new Product
            {
                ProductID = 1,
                Name = "Beige and Red Crochet Dress",
                Description = "A beautiful beige and red crochet dress perfect for summer outings.",
                Price = 95.00m,
                ProductImages = new List<ProductImage>
        {
            new ProductImage
            {
                ImageUrl = "/foto/clothes/f1.jpg"
            }
        }
            };

            return View(product); // passes a single product to Shop.cshtml
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
    }
}
