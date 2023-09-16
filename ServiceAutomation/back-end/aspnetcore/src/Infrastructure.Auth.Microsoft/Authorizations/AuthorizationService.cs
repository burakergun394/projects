using Application.Abstractions.Auth;
using Domain.Shared.Contexts;
using Domain.Shared.Enums;
using Infrastructure.Auth.Microsoft.Extensions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth.Microsoft.Authorizations;

internal class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IExternalContextService _externalContextService;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor, IExternalContextService externalContextService)
    {
        _httpContextAccessor = httpContextAccessor;
        _externalContextService = externalContextService;
    }

    public Task<bool> IsAuthorizeAsync(string claim)
    {
        if (_httpContextAccessor is null || _httpContextAccessor.HttpContext is null)
            return Task.FromResult(false);

        var expirationTime = _httpContextAccessor.HttpContext.User.Claims("exp").FirstOrDefault();
        if (string.IsNullOrWhiteSpace(expirationTime))
            return Task.FromResult(false);

        var expirationTimeDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationTime));
        if (expirationTimeDate < DateTimeOffset.UtcNow)
            return Task.FromResult(false);

        var claims = _httpContextAccessor.HttpContext.User.Claims();

        if (claims is null)
            return Task.FromResult(false);

        if (string.IsNullOrWhiteSpace(claim))
            return Task.FromResult(false);

        var value = claims.FirstOrDefault(x => x == claim.ToUpperInvariant());

        if (value is null || string.IsNullOrWhiteSpace(value))
            return Task.FromResult(false);

        var tenantCode = _httpContextAccessor.HttpContext.User.Claims("tenantCode").FirstOrDefault();
        var userName = _httpContextAccessor.HttpContext.User.Claims("userName").FirstOrDefault();
        _externalContextService.SetContext(new ContextMetadaData
        {
            Username = userName,
            TenantCode = tenantCode,
        });

        return Task.FromResult(true);
    }
}
