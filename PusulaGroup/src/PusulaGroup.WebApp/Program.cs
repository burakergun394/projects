using Microsoft.AspNetCore.Authentication.Cookies;
using PusulaGroup.WebApp.Core.DependencyResolvers;
using PusulaGroup.WebApp.Core.Helpers;
using PusulaGroup.WebApp.Core.Middlewares;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.DependencyResolvers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Admin/Login";
                  options.ExpireTimeSpan = TimeSpan.FromDays(2);
                  options.SlidingExpiration = true;
                  options.Cookie.SameSite = SameSiteMode.Strict;
                  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                  options.Cookie.IsEssential = true;
                  options.Cookie.HttpOnly = true;
                  options.Cookie.Name = "PusulaGroup";
              });
builder.Services.AddRazorPages();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddAuthorization();

builder.Services.AddCoreLayerServices(builder.Configuration);
builder.Services.AddInfrastructureLayerServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
