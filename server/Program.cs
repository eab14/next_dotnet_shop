using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<NextNetDatabaseSettings>(
    builder.Configuration.GetSection("NextNetDatabase"));

builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<FeaturedProductsService>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<OrdersService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5245",
            ValidAudience = "http://localhost:3000",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here12345678901234567"))
        };
    });

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var usersService = services.GetRequiredService<UsersService>();
    await usersService.SeedDatabase();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
