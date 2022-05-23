using IdentityModel;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services; 
services.AddDistributedMemoryCache();

services.AddAuthentication("token")
    .AddOAuth2Introspection("token", options =>
    {
        options.Authority = "https://localhost:5131"; 
      
        options.ClientId = "ApiTwo";
        options.ClientSecret = "api2_secret"; 

        options.EnableCaching = true;
        options.CacheDuration = TimeSpan.FromSeconds(60);

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