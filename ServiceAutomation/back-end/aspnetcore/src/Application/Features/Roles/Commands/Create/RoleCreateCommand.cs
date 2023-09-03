using Ardalis.GuardClauses;
using Domain.Claims;
using Domain.Responses;
using Domain.Roles;
using FluentValidation;
using MediatR;

namespace Application.Features.Roles.Commands.Create;

public record RoleCreateCommand(string Name) : IRequest<Response<Role>>;

public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, Response<Role>>
{
    private readonly IRoleRepository _roleRepository;

    public RoleCreateCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Response<Role>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _roleRepository.IsRoleExistByName(request.Name);
        if (isExist)
            Response<Role>.Failure($"Cannot add a duplicate role name({request.Name})");

        var adding = Role.Create(request.Name);
        var added = await _roleRepository.CreateAsync(adding, cancellationToken);
        await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<Role>.Success(added);
    }
}

public class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
{
    public RoleCreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required");
    }
}