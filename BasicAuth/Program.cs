var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication();
services.AddAuthentication();
services.AddControllers();

var app = builder.Build(); 

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  
    endpoints.MapDefaultControllerRoute();
    
});

app.Run();
