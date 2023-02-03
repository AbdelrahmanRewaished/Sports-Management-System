
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddRazorPages().AddRazorRuntimeCompilation();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});
services.AddControllersWithViews();
services.AddDbContext<ChampionsLeagueDbContext>(option => option.UseSqlServer(connection));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
