using Microsoft.EntityFrameworkCore;
using Sports_Management_System;
using Sports_Management_System.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables()
                     .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

var services = builder.Services;
// Add services to the container.
services.AddRazorPages().AddRazorRuntimeCompilation();
services.AddSingleton<IConfiguration>(builder.Configuration);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ChampionsLeagueDbContext>(option => option.UseSqlServer(connection));



string cookieSecret = builder.Configuration.GetSection("COOKIE_AUTH").Value!;

services.AddAuthentication().AddCookie(cookieSecret, options =>
{
    options.Cookie.Name = cookieSecret;
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login/UnAuthorized";
});

services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromHours(6);
});

services.AddControllersWithViews();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
