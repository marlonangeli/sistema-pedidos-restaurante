using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Data.Context;
using Restaurante.Cheng.Data.Repositories;
using Restaurante.Cheng.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RestauranteDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("RestauranteDb"))
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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