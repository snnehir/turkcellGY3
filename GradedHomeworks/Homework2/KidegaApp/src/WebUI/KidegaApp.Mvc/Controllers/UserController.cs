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
            if (User.Identity.IsAuthenticated)
            {   
                if(!string.IsNullOrEmpty(redirectUrl))
                    return Redirect(redirectUrl);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.RedirectUrl = redirectUrl;
                return View();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model, string? redirectUrl)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await userService.ValidateUserLogin(model.Adapt<UserLoginRequest>());
                if (loginResponse != null && loginResponse.IsSuccess)
                {
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Email, loginResponse.Email),
                        new Claim(ClaimTypes.Role, loginResponse.Role),
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
        [AllowAnonymous]
        public IActionResult SignUp(string? redirectUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(redirectUrl))
                    return Redirect(redirectUrl);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.RedirectUrl = redirectUrl;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model, string? redirectUrl)
        {
            if (ModelState.IsValid)
            {
                var signUp = await userService.ValidateUserSignUp(model.Adapt<UserSignUpRequest>());
                if (signUp != null && signUp.IsSuccess)
                {
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Email, signUp.Email),
                        new Claim(ClaimTypes.Role, signUp.Role),
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
                ModelState.AddModelError("signup", "Invalid email or password!");
            }
            return View();
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
