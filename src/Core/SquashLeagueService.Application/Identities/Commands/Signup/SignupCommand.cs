using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Exceptions;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Application.Identities.Commands.Signup;

public class SignupCommand : IRequest<SignupResponse>
{
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Password { get; init; }
}

public class SignupQueryHandler : IRequestHandler<SignupCommand, SignupResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SignupQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<SignupResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser is { })
            throw new UserAlreadyExistsException(request.Username);

        return new SignupResponse();
    }
}

