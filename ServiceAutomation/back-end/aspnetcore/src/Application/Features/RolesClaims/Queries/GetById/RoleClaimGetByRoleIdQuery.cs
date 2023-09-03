using Domain.Responses;
using Domain.RolesClaims;
using MediatR;

namespace Application.Features.RolesClaims.Queries.GetById;

public record RoleClaimGetByRoleIdQuery(Guid RoleId) : IRequest<Response<RoleClaim>>;

public class RoleClaimGetByRoleIdQueryHandler : IRequestHandler<RoleClaimGetByRoleIdQuery, Response<RoleClaim>>
{
    private readonly IRoleClaimRepository _roleClaimRepository;

    public RoleClaimGetByRoleIdQueryHandler(IRoleClaimRepository roleClaimRepository)
    {
        _roleClaimRepository = roleClaimRepository;
    }

    public async Task<Response<RoleClaim>> Handle(RoleClaimGetByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var roleClaim = await _roleClaimRepository.GetByRoleIdAsync(request.RoleId, cancellationToken);
        return roleClaim is null ? Response<RoleClaim>.Failure("Role claim not found.") : Response<RoleClaim>.Success(roleClaim);
    }
}