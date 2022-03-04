using Microsoft.IdentityModel.Tokens;
using SquashLeagueService.Application.Identities.Queries.SignIn;

namespace SquashLeagueService.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> SignInAsync(SignInQuery signInQuery);
}