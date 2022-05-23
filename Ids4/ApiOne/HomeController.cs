using IdentityModel.Client;
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

       
        public async Task<string> RequestSecret()
        {
            var server = "https://localhost:5131";

            var apiOne = "http://localhost:5030";

            var client = new HttpClient();

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(server);

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {

                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne" 
            });

            var token = tokenResponse.AccessToken;

            client.SetBearerToken(token);

            var response = await client.GetStringAsync(apiOne + "/Home/Secret");

            return response;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            var user = HttpContext.User;

            return Content("Secret");
        } 
 
    }
}