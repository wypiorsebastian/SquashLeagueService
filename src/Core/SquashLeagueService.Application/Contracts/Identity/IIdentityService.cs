using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SquashLeagueService.Application.Identities.Queries.SignIn;
using SquashLeagueService.Application.Users.Queries.GetUser;

namespace SquashLeagueService.Application.Contracts.Identity;

public interface IIdentityService
{
    Task<AuthenticationResponse> SignInAsync(SignInQuery signInQuery);
    Task<IEnumerable<string>> GetUserRoles(string userId);
}