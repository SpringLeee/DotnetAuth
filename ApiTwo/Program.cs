using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build(); 

app.Map("/Go", async context => {

    var server = "https://localhost:5131";

    var apiOne = "http://localhost:5030"; 

    var client = new HttpClient();

    var discoveryDocument = await client.GetDiscoveryDocumentAsync(server);

    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest { 
    
         Address = discoveryDocument.TokenEndpoint,
         ClientId = "client_id",
         ClientSecret = "client_secret", 
         Scope = "ApiOne" 
    });

    var token = tokenResponse.AccessToken;   

    client.SetBearerToken(token); 

    var response = await client.GetStringAsync(apiOne + "/Home/Secret");

    await context.Response.WriteAsync(response); 

});

app.Run();
