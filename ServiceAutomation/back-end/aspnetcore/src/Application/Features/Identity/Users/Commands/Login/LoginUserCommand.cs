using Application.Abstractions.Auth;
using Application.Abstractions.Localizations;
using Application.Features.Identity.Users.Commands.Create;
using Domain.Identity.Users;
using Domain.Shared.Helpers;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Users.Commands.Login;

public record LoginUserCommand(string TenantCode,
                               string Username,
                               string Password) : IRequest<Response<LoginUserCommandResponse>>;

public record LoginUserCommandResponse(string? AuthToken,
                                       DateTime AccessTokenExpireDate);

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<LoginUserCommandResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly ILocalizationService _localizationService;

    public LoginUserCommandHandler(ITokenService tokenService, IUserRepository userRepository, ILocalizationService localizationService)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var getPassword = await _userRepository.GetPasswordAsync(request.TenantCode, request.Username, cancellationToken);

        if (getPassword is null)
            return Response<LoginUserCommandResponse>.Failure(_localizationService["UserNotFound"]);

        if (!PasswordHasherHelper.VerifyPassword(request.Password, getPassword.PasswordHash, getPassword.PasswordSalt))
            return Response<LoginUserCommandResponse>.Failure(_localizationService["PasswordWrong"]);

        var claims = await _userRepository.GetClaimsAsync(request.TenantCode, request.Username, cancellationToken);

        var tokenCreateResponse = await _tokenService.CreateAsync(new TokenCreateRequest(request.Username, claims, request.TenantCode));

        return Response<LoginUserCommandResponse>.Success(new LoginUserCommandResponse(tokenCreateResponse.Token,tokenCreateResponse.TokenExpireDate), _localizationService["Created:{0}", "Token"]);
    }
}

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.TenantCode)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.TenantCode)]);

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Username)]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Password)]);
    }
}