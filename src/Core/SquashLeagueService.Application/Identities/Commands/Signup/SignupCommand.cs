using System.Security.Claims;
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
    public string Role { get; init; }
    public string PhoneNumber { get; init; }
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

        await VerifyUserExistence(request);

        var createdUser = await CreateApplicationUser(request);
        
        await _userManager.AddToRoleAsync(createdUser, "Player");
        await AddUserClaims(createdUser, request);

        return new SignupResponse { Id = createdUser.Id};
    }

    private async Task<ApplicationUser> CreateApplicationUser(SignupCommand request)
    {
        var newUser = new ApplicationUser()
        {
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var userCreationResult = await _userManager.CreateAsync(newUser, request.Password);
        if (!userCreationResult.Succeeded)
            throw new UserRegistrationException("Registration error", userCreationResult.Errors);
        
        return await _userManager.FindByNameAsync(request.Username);
    }

    private async Task VerifyUserExistence(SignupCommand request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser is { })
            throw new UserAlreadyExistsException(request.Username);

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail is { })
            throw new UserAlreadyExistsException(request.Email);
    }

    private Task AddUserClaims(ApplicationUser applicationUser, SignupCommand request)
    {
        var tasks = new List<Task>();
        tasks.Add(_userManager.AddClaimAsync(applicationUser, new Claim("Phone", request.PhoneNumber)));
        tasks.Add(_userManager.AddClaimAsync(applicationUser, new Claim("Email", request.Email)));

        return Task.WhenAll(tasks);
    }
}

