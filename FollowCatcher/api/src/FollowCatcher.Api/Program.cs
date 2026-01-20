using FollowCatcher.Application;
using FollowCatcher.Persistence;
using FollowCatcher.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load local configuration (for secrets, not committed to git)
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers();

// Add Application layer services
builder.Services.AddApplication();

// Add Persistence layer services
builder.Services.AddPersistence(builder.Configuration);

// Add Infrastructure layer services
builder.Services.AddInfrastructure(builder.Configuration);

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "FollowCatcher API", Version = "v1" });
});

// Add CORS if needed
// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(policy =>
//     {
//         policy.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//     });
// });

var app = builder.Build();

// Apply database migrations (creates database and tables if they don't exist)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FollowCatcher API v1");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FollowCatcher API v1");
        options.RoutePrefix = "swagger";
    });

    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }
}

app.UseHttpsRedirection();

// Enable CORS if configured
// app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
