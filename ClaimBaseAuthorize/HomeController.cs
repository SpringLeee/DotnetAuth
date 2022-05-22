using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ClaimBaseAuthorize
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

        public async Task<IActionResult> Login(string user)
        {
            if (string.IsNullOrEmpty(user)) return NoContent();

            var claims = new List<Claim> {

                new Claim(ClaimTypes.Name,user), new Claim(ClaimTypes.Role, user),
                
            };    

            var identity = new ClaimsIdentity(claims, "Identity");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            
            return Content("Login Success");
        }
        
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();

            return Content("Logout Success");
        } 

        [Authorize]
        public IActionResult Secret()
        {
            var user = HttpContext.User;

            return Content("Secret");
        }


        [Authorize]
        public IActionResult SecretForDev()
        { 
            return Content("SecretForDev");
        }

        [Authorize]
        public IActionResult SecretForAdmin()
        {
            return Content("SecretForAdmin");
        } 
    }
}