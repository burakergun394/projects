using Application.Abstractions.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth.Microsoft.Tokens;

internal class JwtTokenService : ITokenService
{
    private readonly TokenOptions _options;

    public JwtTokenService(TokenOptions options)
    {
        _options = options;
    }

    public Task<TokenCreateResponse> CreateAsync(TokenCreateRequest request)
    {
        var bytes = Encoding.UTF8.GetBytes(_options.Secret);
        var symmetricSecurityKey = new SymmetricSecurityKey(bytes);
        var dateTimeUtcNow = DateTime.UtcNow;
        var expireDate = dateTimeUtcNow.Add(TimeSpan.FromMinutes(500));

        var claims = new List<Claim>
        {
            new Claim("userName", request.Username),
            new Claim("tenantCode", request.TenantCode)
        };

        if (request.Claims is not null && request.Claims.Any())
            claims.Add(new Claim("claims", string.Join(";", request.Claims)));

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: dateTimeUtcNow,
                expires: expireDate,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        var tokenCreateResponse = new TokenCreateResponse(token, expireDate);

        return Task.FromResult(tokenCreateResponse);
    }
}
