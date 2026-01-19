using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZELDA.Data;
using ZELDA.Models;
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

        //  Cart page
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        //Add product to cart
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductID == id);

            var imageUrl = product.ProductImages?.FirstOrDefault()?.ImageUrl;

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
                    ImageUrl = imageUrl,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        //Remove item completely
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

        //Increase quantity
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

        //Decrease quantity
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

        //Session helpers

        private CartViewModel GetCart()
        {
            try
            {
                var cartJson = HttpContext.Session.GetString("Cart");

                if (string.IsNullOrEmpty(cartJson))
                    return new CartViewModel();

                // Safely deserialize
                var cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);
                return cart ?? new CartViewModel(); // fallback if deserialization fails
            }
            catch
            {
                // If something goes wrong (invalid JSON, etc.)
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
                // ignore failures, log errors here
            }
        }
    }
}
