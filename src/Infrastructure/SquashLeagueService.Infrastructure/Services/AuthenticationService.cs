using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Application.Identities.Queries.SignIn;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Infrastructure.Services.TokenService;

namespace SquashLeagueService.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<AuthenticationResponse> SignInAsync(SignInQuery signInQuery)
    {
        var applicationUser = await _userManager.FindByNameAsync(signInQuery.Username);
        
        if (applicationUser is null)
            throw new UserAuthenticationException("Provided user does not exist");

        var isPasswordValid = await _userManager.CheckPasswordAsync(applicationUser, signInQuery.Password);
        if (isPasswordValid is false)
            throw new UserAuthenticationException("Provided credentials are invalid");

        var jwtToken = await  _tokenService.GenerateToken(applicationUser);

        var authResponse = new AuthenticationResponse(applicationUser.Id, applicationUser.Email, applicationUser.Email, new JwtSecurityTokenHandler().WriteToken(jwtToken));

        return authResponse;
    }
}