using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Application.Features.Identity.Users.Commands.Create;
using Ardalis.GuardClauses;
using Domain.Identity.Roles;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.Create;

public record RoleCreateCommand(string Name) : IRequest<Response<Role>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "role.create";
}

public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, Response<Role>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILocalizationService _localizationService;

    public RoleCreateCommandHandler(IRoleRepository roleRepository, ILocalizationService localizationService)
    {
        _roleRepository = roleRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<Role>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _roleRepository.IsRoleExist(request.Name);
        if (isExist)
            return Response<Role>.Failure(_localizationService["RoleAlreadyExist:{0}", request.Name]);

        var adding = Role.Create(request.Name);
        var added = await _roleRepository.CreateAsync(adding, cancellationToken);
        await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<Role>.Success(added, _localizationService["RoleCreated:{0}", request.Name]);
    }
}

public class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
{
    public RoleCreateCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Name)]);
    }
}