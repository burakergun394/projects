using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

var host = CreateHostBuilder(args).Build();

var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.EnsureCreatedAsync();
Console.WriteLine(dbContext.Database.GenerateCreateScript());

Console.ReadLine();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables();
            var configurationRoot = builder.Build();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection"));
            });
        });