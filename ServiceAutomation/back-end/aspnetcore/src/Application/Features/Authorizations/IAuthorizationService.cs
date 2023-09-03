namespace Application.Features.Authorizations;

public interface IAuthorizationService
{
    Task<bool> IsAuthorizeAsync(string claim);
}
