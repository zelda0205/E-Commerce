using Microsoft.AspNetCore.Mvc;

namespace ZELDA.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
