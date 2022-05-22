using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication(x => {

    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(x => { 
    
    

});

services.AddAuthorization(x => {

    Func<AuthorizationHandlerContext, bool> handler = (context) =>
    {  
        if (context.User.HasClaim(ClaimTypes.Name, "Dev") || context.User.HasClaim(ClaimTypes.Name, "Admin"))
        {
            return true;
        } 
        
        return false;    
    }; 

    x.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAssertion(handler).Build();   
    
});

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
