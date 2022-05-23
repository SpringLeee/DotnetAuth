using IdentityModel;
using IdentityServer;
using Microsoft.IdentityModel.Tokens; 

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddIdentityServer()
    .AddInMemoryApiScopes(Configuration.GetScopes())
    .AddInMemoryApiResources(Configuration.GetApis())
    .AddInMemoryClients(Configuration.GetClients())
    .AddDeveloperSigningCredential();

services.AddControllers();
var app = builder.Build();

app.UseRouting();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();

});

app.Run();
