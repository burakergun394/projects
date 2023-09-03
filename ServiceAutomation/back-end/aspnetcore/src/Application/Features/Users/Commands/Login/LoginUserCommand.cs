using Application.Features.Tokens;
using Application.Helpers;
using Domain.Responses;
using Domain.Users;
using MediatR;

namespace Application.Features.Users.Commands.Login;

public record LoginUserCommand(string UserName,
                                      string Password) : IRequest<Response<LoginUserCommandResponse>>;

public record LoginUserCommandResponse(string? AuthToken,
                                       DateTime AccessTokenExpireDate);

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<LoginUserCommandResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<Response<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {

        var getPassword = await _userRepository.GetPasswordByUsernameAsync(request.UserName, cancellationToken);

        if (getPassword is null)
            return Response<LoginUserCommandResponse>.Failure("User or password wrong.");

        if (!PasswordHasherHelper.VerifyPassword(request.Password, getPassword.PasswordHash, getPassword.PasswordSalt))
            return Response<LoginUserCommandResponse>.Failure("User or password wrong.");

        var claims = await _userRepository.GetClaimsByUsername(request.UserName, cancellationToken);

        var tokenCreateResponse = await _tokenService.CreateAsync(new TokenCreateRequest(request.UserName, claims));

        return Response<LoginUserCommandResponse>.Success(new LoginUserCommandResponse(tokenCreateResponse.Token,
                                                tokenCreateResponse.TokenExpireDate));
    }
}