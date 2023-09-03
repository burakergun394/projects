namespace Application.Behaviors.Authorization;

internal interface IAuthorizationClaimRequest
{
    string Claim { get; }
}
