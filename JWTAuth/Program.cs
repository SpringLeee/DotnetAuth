using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => {

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAuth.Constants.Secret));

        x.TokenValidationParameters = new TokenValidationParameters
        { 
            ValidIssuer = JWTAuth.Constants.Issuer,
            ValidAudience = JWTAuth.Constants.Audiance,
            IssuerSigningKey = key
        };

    });

services.AddAuthorization();  
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
