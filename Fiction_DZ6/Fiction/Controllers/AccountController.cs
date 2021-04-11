using Fiction_DZ6.Models;
using Fiction_DZ6.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiction_DZ6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]//method will ignore authorize controller tag
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };

                var createTask = _userManager.CreateAsync(user, viewModel.Password);

                if (createTask.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in createTask.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInTask = _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);

                if (signInTask.Result.Succeeded)
                {

                    if (returnUrl is not null)
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Incorrect username or password");
            }

            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
