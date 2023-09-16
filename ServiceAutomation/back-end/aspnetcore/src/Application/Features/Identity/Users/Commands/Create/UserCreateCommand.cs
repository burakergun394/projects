using Application.Abstractions.Localizations;
using Ardalis.GuardClauses;
using Domain.Identity.Users;
using Domain.Shared.Helpers;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Users.Commands.Create;

public record UserCreateCommand(string TenantCode, string Username, Guid RoleId, string Name, string Surname, string Email, string Password) : IRequest<Response<User>>;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Response<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILocalizationService _localizationService;

    public UserCreateCommandHandler(IUserRepository userRepository, ILocalizationService localizationService)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<User>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _userRepository.IsUserExistAsync(request.TenantCode, request.Username, cancellationToken);
        if (isExist)
            return Response<User>.Failure(_localizationService["UserAlreadyExist:{0}", request.Username]);

        // TO DO Email Check

        PasswordHasherHelper.HashAndSaltPassword(request.Password, out var hash, out var salt);
        var addingUser = User.Create(request.TenantCode, request.Username, request.RoleId, request.Name, request.Surname, request.Email, hash, salt);
        var addedUser = await _userRepository.CreateAsync(addingUser, cancellationToken);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<User>.Success(addedUser, _localizationService["UserCreated:{0}", request.Username]);
    }
}

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.TenantCode)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.TenantCode)]);

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Username)]);

        RuleFor(x => x.RoleId)
           .NotEmpty()
           .NotNull()
           .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.RoleId)]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Name)]);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Surname)]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Password)]);

        const int LengthOfMinimumPassword = 8;

        RuleFor(x => x.Password)
            .MinimumLength(LengthOfMinimumPassword)
            .WithMessage(localizationService["MinimumLength:{0}{1}", nameof(UserCreateCommand.Password), LengthOfMinimumPassword]);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Email)]);
    }
}