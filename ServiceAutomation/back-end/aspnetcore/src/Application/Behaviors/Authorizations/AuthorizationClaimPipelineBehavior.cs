using Application.Abstractions.Auth;
using Application.Exceptions;
using MediatR;

namespace Application.Behaviors.Authorization;

internal class AuthorizationClaimPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IAuthorizationClaimRequest
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationClaimPipelineBehavior(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!await _authorizationService.IsAuthorizeAsync(request.Claim))
            throw new AuthorizationException("You are not authorized.");

        return await next();
    }
}
