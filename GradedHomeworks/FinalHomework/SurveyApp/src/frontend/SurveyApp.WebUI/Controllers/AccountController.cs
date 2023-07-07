using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.WebUI.Models.Requests;
using SurveyApp.WebUI.Services.User;

namespace SurveyApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login(string? redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(redirectUrl) && Url.IsLocalUrl(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequestModel registerModel)
        {
            var signupResponse = await _userService.SignUpAsync(registerModel);
            if(!signupResponse.Succeeded)
            {
                ViewBag.ErrorMessage = signupResponse.message;
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginModel, string? redirectUrl)
        {
            var loginResponse = await _userService.LoginAsync(loginModel);

            if (!loginResponse.Succeeded)
            {
                ViewBag.ErrorMessage = loginResponse.message;
                return View();
            }
            if (!string.IsNullOrEmpty(redirectUrl) && Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
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
