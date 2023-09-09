namespace Application.Features.Tokens;

public interface ITokenService
{
    Task<TokenCreateResponse> CreateAsync(TokenCreateRequest request);
}

public record TokenCreateRequest(string Username, List<string> Claims);

public record TokenCreateResponse(string Token, DateTime TokenExpireDate);

public class TokenOptions
{
    public string Issuer { get; set; } = "AudienceInformation";
    public string Audience { get; set; } = "IssuerInformation";
    public string Secret { get; set; } = "SecretInformation";
}