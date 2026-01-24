using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ZELDA.Data;
using ZELDA.ViewModels;

namespace ZELDA.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
        [Authorize(Roles = "Admin, User")]
        public IActionResult AddToCart(int id)
        {
            if (User != null && User.Identity != null && !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var product = _context.Products
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductID == id);

          
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = "/ProductsImages/" + product.Image,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductID == id);

            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Increase(int id)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductID == id);

            if (item != null)
            {
                item.Quantity++;
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
       
        [Authorize(Roles = "Admin,User")]
        public IActionResult Decrease(int id)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductID == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                    cart.Items.Remove(item);

                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IActionResult ConfirmCheckout()
        {
            var cart = GetCart();
            return View(cart);
        }

        private CartViewModel GetCart()
        {
            try
            {
                var cartJson = HttpContext.Session.GetString("Cart");

                if (string.IsNullOrEmpty(cartJson))
                    return new CartViewModel();

                var cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);

                return cart ?? new CartViewModel(); 
            }
            catch
            {
                return new CartViewModel();
            }
        }

        private void SaveCart(CartViewModel cart)
        {
            try
            {
                var cartJson = JsonConvert.SerializeObject(cart);

                HttpContext.Session.SetString("Cart", cartJson);
            }
            catch
            {
                
            }
        }
    }
}
