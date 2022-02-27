using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIDisney2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: HomeController
        public IActionResult Register()
        {
            return View();
        }
        [Route("/auth/register")]
        [HttpPost("Login")]
        public async Task<IActionResult> Register(string txtEmail, string txtPassword)
        {
            if(!string.IsNullOrEmpty(txtEmail))
            {
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, txtEmail),
                };
                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                return View(true);
            }
            return View(false);
        }
        [Route("/auth/login")]
        public async Task<IActionResult> Login(string txtEmail, string txtPassword)
        {
            if (!string.IsNullOrEmpty(txtEmail))
            {
                /*
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, txtEmail),
                };
                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
                */
                return View(true);
            }
            return View(false);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Register");
        }
    }
}
