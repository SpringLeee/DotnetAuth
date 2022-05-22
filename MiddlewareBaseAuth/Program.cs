using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using MiddlewareBaseAuth;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSingleton<IAuthorizationMiddlewareResultHandler,CustomAuthorizationMiddleware>();

services.AddAuthentication(x => {

    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(x => { 
    

});

services.AddAuthorization();

services.AddControllers(); 
var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();

});

app.Run();
