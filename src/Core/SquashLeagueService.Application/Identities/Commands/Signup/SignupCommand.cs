using MediatR;
using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Exceptions;

namespace SquashLeagueService.Application.Identities.Commands.Signup;

public class SignupCommand : IRequest<SignupResponse>
{
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
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
        if (!(await _roleManager.RoleExistsAsync("player")))
        {
            var role = new IdentityRole { Name = "Player" };
            var roleCreationResult = await _roleManager.CreateAsync(role);

            if (!roleCreationResult.Succeeded)
                throw new RoleRegistrationException(role.Name);
        }
        
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser is { })
            throw new UserAlreadyExistsException(request.Username);

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail is { })
            throw new UserAlreadyExistsException(request.Email);

        var newUser = new ApplicationUser()
        {
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var userCreationResult = await _userManager.CreateAsync(newUser, request.Password);
        if (!userCreationResult.Succeeded)
            throw new UserRegistrationException("Registration error", userCreationResult.Errors);

        var createdUser = await _userManager.FindByNameAsync(request.Username);
        await _userManager.AddToRoleAsync(createdUser, "Player");

        return new SignupResponse { Id = createdUser.Id};
    }
}

