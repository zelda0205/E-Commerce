using Microsoft.AspNetCore.Mvc;

namespace ZELDA.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult RegisterLogin()
        {
            return View();
        }
    }
}
