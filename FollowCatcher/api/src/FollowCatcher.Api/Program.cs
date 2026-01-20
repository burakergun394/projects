using FollowCatcher.Application;
using FollowCatcher.Persistence;
using FollowCatcher.Infrastructure;
using FollowCatcher.Api.Middleware;
using FollowCatcher.Api.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Load local configuration (for secrets, not committed to git)
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers();

// Add Application layer services
builder.Services.AddApplication(builder.Configuration);

// Add Persistence layer services
builder.Services.AddPersistence(builder.Configuration);

// Add Infrastructure layer services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");

// Add Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 10
            }));

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "FollowCatcher API", Version = "v1" });
});

var app = builder.Build();

// Apply database migrations (creates database and tables if they don't exist)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Global Exception Handler (must be first in pipeline)
app.UseGlobalExceptionHandler();

// Configure Swagger (enabled in all environments)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FollowCatcher API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// Enable Rate Limiting
app.UseRateLimiter();

app.UseAuthorization();

// Map Health Check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
