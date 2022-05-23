using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource {
                
                     Name = "ApiOne",
                     Scopes = { "ApiOne" }

                }
            };
        }


        public static IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope> {  
                new ApiScope("ApiOne")
            }; 
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client { 
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) }, 
                    ClientClaimsPrefix = "",  
                    Claims = { new ClientClaim(JwtClaimTypes.Role,"Admin"), new ClientClaim(JwtClaimTypes.Name,"Lee") },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,  
                    AllowedScopes = { "ApiOne" }
                }
            };
        }


    }
}
