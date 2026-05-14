using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZELDA.Data;
using ZELDA.Models;
using ZELDA.ViewModels;

namespace ZELDA.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var cartDetails = GetCart(); 

            if (cartDetails.Items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

          
            var userEmail = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return Unauthorized();


            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = cartDetails.GrandTotal,
                OrderItems = cartDetails.Items.Select(item => new OrderItem
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); 

            cartDetails.OrderID = order.OrderID;
            return View("ConfirmCheckout", cartDetails);
        }

        [Authorize]
        public async Task<IActionResult> OrderSuccessfull(int id, string payPalId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);

            if (order != null)
            {
                order.PayPalOrderId = payPalId;
                HttpContext.Session.Remove("Cart");
                return View( order);
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                 .Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}