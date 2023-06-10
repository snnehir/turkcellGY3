using KidegaApp.DataTransferObjects.Requests;
using KidegaApp.Mvc.Models;
using KidegaApp.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KidegaApp.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string? redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model, string? redirectUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.ValidateUser(model.Adapt<UserLoginRequest>());
                if (user != null)
                {
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    if (!string.IsNullOrEmpty(redirectUrl) && Url.IsLocalUrl(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("login", "Wrong username or password!");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
