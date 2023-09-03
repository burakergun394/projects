using Application.Features.Authorizations;
using Infrastructure.Auth.Microsoft.Extensions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth.Microsoft.Authorizations;

internal class AuthorizationService: IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> IsAuthorizeAsync(string claim)
    {
        if (_httpContextAccessor is null || _httpContextAccessor.HttpContext is null)
            return Task.FromResult(false);

        var claims = _httpContextAccessor.HttpContext.User.Claims();

        if (claims is null)
            return Task.FromResult(false);

        if (string.IsNullOrWhiteSpace(claim))
            return Task.FromResult(false);

        var value = claims.FirstOrDefault(x => x == claim.ToUpperInvariant());

        if (value is null || string.IsNullOrWhiteSpace(value))
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}
