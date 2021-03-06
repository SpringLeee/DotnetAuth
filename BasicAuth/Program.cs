using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication(x => {
    
    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; 

}).AddCookie(x => { 
     

});

services.AddAuthorization(x => {  

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
