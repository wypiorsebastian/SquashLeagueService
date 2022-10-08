using SquashLeagueService.Application.Identities.Commands.Signup;
using SquashLeagueService.Application.Identities.Queries.SignIn;

namespace SquashLeagueService.Application.Contracts.Identity;

public interface IIdentityService
{
    Task<AuthenticationResponse> SignInAsync(SignInQuery signInQuery);
    Task<SignupResponse> SignupAsync(SignupCommand signupCommand);
}