using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Application.Identities.Queries.SignIn;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<AuthenticationResponse> SignInAsync(SignInQuery signInQuery)
    {
        var applicationUser = await _userManager.FindByNameAsync(signInQuery.Username);
        if (applicationUser is null)
            throw new UserAuthenticationException("Provided user does not exist");

        var isPasswordValid = await _userManager.CheckPasswordAsync(applicationUser, signInQuery.Password);
        if (isPasswordValid is false)
            throw new UserAuthenticationException("Provided credentials are invalid");

        return null;
    }
}