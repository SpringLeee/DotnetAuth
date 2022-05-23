
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ApiForReferenceToken
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return Content("Index");
        } 

       
        public async Task<string> RequestSecret()
        {
            var server = "https://localhost:5131";

            var apiOne = "http://localhost:5801"; 

            var token = _cache.GetString("Token");

            var client = new HttpClient();

            if (string.IsNullOrEmpty(token))
            { 
                var discoveryDocument = await client.GetDiscoveryDocumentAsync(server);

                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                { 
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id_2",
                    ClientSecret = "client_secret_2",
                    Scope = "ApiTwo" 

                });

                token = tokenResponse.AccessToken;

                _cache.SetString("Token", token); 
            } 

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