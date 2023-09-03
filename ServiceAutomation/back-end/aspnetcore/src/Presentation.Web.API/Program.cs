using Application.Extensions;
using Infrastructure.Auth.Microsoft.Extensions;
using Persistence.EntityFrameworkCore.Extensions;
using Presentation.Web.API.Controller.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddPresentationControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddEntityFrameworkCoreServices(builder.Configuration);
builder.Services.AddAuthMicrosoftServices(builder.Configuration);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseEntityFrameworkCoreMigrate();
}

app.UseHttpsRedirection();

app.UseAuthMicrosoft();

app.MapControllers();

app.Run();

