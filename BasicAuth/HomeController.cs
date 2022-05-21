using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BasicAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("Index");
        }

        public async Task<IActionResult> Login()
        {
            var sign = HttpContext.Request.Query["sign"];

            if (string.IsNullOrWhiteSpace(sign))
            {
                return Content("Login");
            }
            else
            {
                var claims = new List<Claim> {

                    new Claim(ClaimTypes.Name, sign)

                };

                var identity = new ClaimsIdentity(claims, "Identity");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return Content("Login2");
            }
        }


        [Authorize]
        public IActionResult Secret()
        {
            return Content("Secret");
        }
    }
}