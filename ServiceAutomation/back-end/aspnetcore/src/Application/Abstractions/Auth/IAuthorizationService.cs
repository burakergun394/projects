namespace Application.Abstractions.Auth;

public interface IAuthorizationService
{
    Task<bool> IsAuthorizeAsync(string claim);
}
