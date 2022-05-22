using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text;

namespace JWTAuth
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
            var claims = new List<Claim>
            {
                new Claim("sub","123456"),
                new Claim(ClaimTypes.Name,"Lee"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"SuperAdmin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken( 
                issuer: Constants.Issuer,
                audience: Constants.Audiance,
                claims: claims,
                expires:DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Content(token);
        }
        
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();

            return Content("Logout Success");
        } 

        [Authorize(Roles = "SuperAdmin")]
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