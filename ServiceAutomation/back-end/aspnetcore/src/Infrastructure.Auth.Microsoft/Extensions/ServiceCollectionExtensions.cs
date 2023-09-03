using Application.Features.Authorizations;
using Application.Features.Tokens;
using Infrastructure.Auth.Microsoft.Authorizations;
using Infrastructure.Auth.Microsoft.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Auth.Microsoft.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthMicrosoftServices(this IServiceCollection services, IConfiguration configuration)
    {

        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
                           ?? new TokenOptions();
        services.AddSingleton(tokenOptions);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var bytes = Encoding.UTF8.GetBytes(tokenOptions.Secret);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(bytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddTransient<ITokenService, JwtTokenService>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddHttpContextAccessor();

        return services;
    }
}