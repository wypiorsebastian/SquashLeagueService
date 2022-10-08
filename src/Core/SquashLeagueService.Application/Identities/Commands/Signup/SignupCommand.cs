using MediatR;
using SquashLeagueService.Application.Contracts.Identity;

namespace SquashLeagueService.Application.Identities.Commands.Signup;

public class SignupCommand : IRequest<SignupResponse>
{
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string[] Roles { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
}

public class SignupQueryHandler : IRequestHandler<SignupCommand, SignupResponse>
{
    private readonly IIdentityService _identityService;

    public SignupQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task<SignupResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var signupResult = await _identityService.SignupAsync(request);

        return new SignupResponse { Id = signupResult.Id};
    }

    
}

