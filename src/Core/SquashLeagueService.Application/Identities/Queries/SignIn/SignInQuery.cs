using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Application.Identities.Queries.SignIn;

public record SignInQuery(string Username, string Password) : IRequest<SignInResponse>;

public class SignInQueryHandler : IRequestHandler<SignInQuery, SignInResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignInQueryHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<SignInResponse> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var applicationUser = await _userManager.FindByNameAsync(request.Username);
        if (applicationUser is null)
            throw new UserAuthenticationException("Provided user does not exist");

        var isPasswordValid = await _userManager.CheckPasswordAsync(applicationUser, request.Password);
        if (isPasswordValid is false)
            throw new UserAuthenticationException("Provided credentials are invalid");
        
        
    }
}
