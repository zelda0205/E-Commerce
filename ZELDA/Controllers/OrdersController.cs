using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var applicationDbContext = _context.Orders.Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create()
        {
            Order order = new Order();

            var cartDetails = GetCart();

            order.OrderDate = DateTime.Now;
            order.TotalAmount = cartDetails.GrandTotal;

            var userEmail = HttpContext!.User!.Identity!.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized();
            }

            order.UserId = user.Id;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            foreach (var item in cartDetails.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                _context.Add(orderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(OrderSuccsessfull));
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult OrderSuccsessfull()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderID == id);
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