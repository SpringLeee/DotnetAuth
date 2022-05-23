using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text;

namespace ApiOne
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
        
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();

            return Content("Logout Success");
        } 

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            var user = HttpContext.User;

            return Content("Secret");
        }


        [AllowAnonymous]
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