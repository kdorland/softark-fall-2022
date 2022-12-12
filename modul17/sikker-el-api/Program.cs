using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddAuthentication("JwtAuthentication")
                .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("JwtAuthentication", null);
builder.Services.AddAuthorization(options => {
    options.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
});

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();

// Cached data while service is running...
var data = "";

app.MapGet("/api/elpriser", [Authorize(Policy= "User")]
    async () => {
        if (!String.IsNullOrEmpty(data)) {
            Console.WriteLine("Returning cached data");
            return Results.Content(data, "application/json"); 
        }

        string apiURL = "https://api.energidataservice.dk/dataset/elspotprices?filter={%22PriceArea%22:%22DK1,DK2%22}&limit=96";
        HttpClient req = new HttpClient();
        var content = await req.GetAsync(apiURL);
        var result = await content.Content.ReadAsStringAsync();
        data = result;

        return Results.Content(result, "application/json"); 
    });    

app.MapGet("/api/hello", [AllowAnonymous]
    () => "Hello World!");

app.MapPost("/api/users/login", [AllowAnonymous]
    (LoginService loginService, TokenService tokenService, LoginData data) => {
        var valid = loginService.ValidateLogin(data.username, data.password);
        Console.WriteLine(valid);
        if (valid) {
            var token = tokenService.GenerateToken(data.username);
            return Results.Json(new { msg = "Login succeded", token = token}, statusCode: 200);
        } else {
            return Results.Json(new { msg = "Login failed" }, statusCode: 401);
        }
    });

Console.WriteLine("App starting up!");

app.Run();

record LoginData(string username, string password);

