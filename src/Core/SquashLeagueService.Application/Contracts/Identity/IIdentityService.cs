using SquashLeagueService.Application.Identities.Commands.Signup;
using SquashLeagueService.Application.Identities.Queries.SignIn;
using SquashLeagueService.Application.Members.Queries.GetMember;
using SquashLeagueService.Application.Models;

namespace SquashLeagueService.Application.Contracts.Identity;

public interface IIdentityService
{
    Task<AuthenticationResult> SignInAsync(SignInQuery signInQuery);
    Task<SignupResponse> SignupAsync(SignupCommand signupCommand);
    Task<IEnumerable<string>> GetIdentityRoles(string identityId);
    Task<IdentityData> GetIdentityData(string identityId);
}