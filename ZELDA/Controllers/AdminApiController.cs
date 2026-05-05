using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZELDA.Data;
using ZELDA.Models;

namespace ZELDA.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/adminapi")]
    public class AdminApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("sales-stats")]
        public async Task<IActionResult> GetSalesStats()
        {
            var now = DateTime.Now;
            var currentMonthSales = await _context.Orders
                .Where(o => o.OrderDate.Month == now.Month && o.OrderDate.Year == now.Year)
                .SumAsync(o => o.TotalAmount);

            var lastMonth = now.AddMonths(-1);
            var lastMonthSales = await _context.Orders
                .Where(o => o.OrderDate.Month == lastMonth.Month && o.OrderDate.Year == lastMonth.Year)
                .SumAsync(o => o.TotalAmount);

            decimal percentageIncrease = 0;
            if (lastMonthSales > 0)
            {
                percentageIncrease = ((currentMonthSales - lastMonthSales) / lastMonthSales) * 100;
            }

            return Ok(new { total = currentMonthSales, increase = Math.Round(percentageIncrease, 1) });
        }

        [HttpGet("orders-by-month")]
        public async Task<IActionResult> GetOrdersByMonth()
        {
            var currentYear = DateTime.Now.Year;
            var ordersData = await _context.Orders
                .Where(o => o.OrderDate.Year == currentYear)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .OrderBy(g => g.Month)
                .ToListAsync();

            string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var labels = new string[12];
            var values = new int[12];

            for (int i = 1; i <= 12; i++)
            {
                labels[i - 1] = monthNames[i - 1];
                var found = ordersData.FirstOrDefault(x => x.Month == i);
                values[i - 1] = found != null ? found.Count : 0;
            }

            return Ok(new { labels, values });
        }
    }
}