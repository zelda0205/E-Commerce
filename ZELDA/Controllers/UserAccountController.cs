using Microsoft.AspNetCore.Mvc;
using ZELDA.ViewModels;
namespace ZELDA.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: User Register
        public IActionResult Register()
        {
            return View("~/Views/UserAccount/Register.cshtml");
        }

        // POST: User Register
        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Add user registration logic here
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View("~/Views/UserAccount/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Add user login logic here
            return RedirectToAction("Index", "Home");
        }
    }
}
