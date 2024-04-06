using server.Models;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<NextNetDatabaseSettings>(
    builder.Configuration.GetSection("NextNetDatabase"));

builder.Services.AddSingleton<ProductsService>();
builder.Services.AddSingleton<FeaturedProductsService>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<OrdersService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var usersService = services.GetRequiredService<UsersService>();
    await usersService.SeedDatabase();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
