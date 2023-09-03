using Domain.Responses;
using Domain.Roles;
using MediatR;

namespace Application.Roles.Queries.GetById;

public record RoleGetByIdQuery(Guid Id) : IRequest<Response<Role>>;

public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, Response<Role>>
{
    private readonly IRoleRepository _roleRepository;
    public RoleGetByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Response<Role>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
        return role is null ? Response<Role>.Failure("Role not found.") : Response<Role>.Success(role);
    }
}