using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public record SignInQuery(string Username, string Password) : IRequest<AuthenticationResponse>;

public class SignInQueryHandler : IRequestHandler<SignInQuery, AuthenticationResponse>
{
    private readonly IIdentityService _authenticationService;

    public SignInQueryHandler(IIdentityService authenticationService)
    {
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    public async Task<AuthenticationResponse> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var authenticationResponse = await _authenticationService.SignInAsync(request);
        return authenticationResponse;
    }
}
