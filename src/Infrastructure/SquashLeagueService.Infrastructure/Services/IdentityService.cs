using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Application.Exceptions;
using SquashLeagueService.Application.Identities.Commands.Signup;
using SquashLeagueService.Application.Identities.Queries.SignIn;
using SquashLeagueService.Application.Members.Queries.GetMember;
using SquashLeagueService.Application.Models;
using SquashLeagueService.Infrastructure.Services.TokenService;

namespace SquashLeagueService.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AuthenticationResult> SignInAsync(SignInQuery signInQuery)
    {
        var identity = await _userManager.FindByNameAsync(signInQuery.Username);
        
        if (identity is null)
            throw new UserAuthenticationException("Provided user does not exist");

        var isPasswordValid = await _userManager.CheckPasswordAsync(identity, signInQuery.Password);
        if (isPasswordValid is false)
            throw new UserAuthenticationException("Provided credentials are invalid");

        var jwtToken = await  _tokenService.GenerateToken(identity);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var authResult = new AuthenticationResult(identity.Id, identity.UserName, identity.Email, tokenValue);

        return authResult;
    }

    public async Task<SignupResponse> SignupAsync(SignupCommand signupCommand)
    {
        if (!(await _roleManager.RoleExistsAsync("Player")))
        {
            var role = new IdentityRole { Name = "Player" };
            var roleCreationResult = await _roleManager.CreateAsync(role);

            if (!roleCreationResult.Succeeded)
                throw new RoleRegistrationException(role.Name);
        }
        
        if (!(await _roleManager.RoleExistsAsync("Admin")))
        {
            var adminRole = new IdentityRole { Name = "Admin" };
            var roleCreationResult = await _roleManager.CreateAsync(adminRole);

            if (!roleCreationResult.Succeeded)
                throw new RoleRegistrationException(adminRole.Name);
        }
        
        await VerifyIdentityExistence(signupCommand);
        

        var createdUser = await CreateIdentity(signupCommand);

        foreach (var role in signupCommand.Roles ?? Enumerable.Empty<string>())
        {
            await _userManager.AddToRoleAsync(createdUser, role);
        }
        
        //await _userManager.AddToRoleAsync(createdUser, "Admin");
        await AddIdentityClaims(createdUser, signupCommand);

        return new SignupResponse { Id = createdUser.Id};
    }
    
    public async Task<IEnumerable<string>> GetIdentityRoles(string identityId)
    {
        var member = await _userManager.FindByIdAsync(identityId);

        if (member is null)
            throw new MemberDoesNotExistException(identityId);

        return await _userManager.GetRolesAsync(member);
    }

    public async Task<IdentityData> GetIdentityData(string identityId)
    {
        var identity = await _userManager.FindByIdAsync(identityId);

        if (identity is null)
            throw new IdentityDoesNotExistsException(identityId);


        return new IdentityData(identity.UserName, identity.PhoneNumber, identity.Email);
    }

    private async Task VerifyIdentityExistence(SignupCommand request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser is { })
            throw new UserAlreadyExistsException(request.Username);

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail is { })
            throw new UserAlreadyExistsException(request.Email);
    }
    
    private async Task<IdentityUser> CreateIdentity(SignupCommand request)
    {
        var newUser = new IdentityUser()
        {
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
        };

        var userCreationResult = await _userManager.CreateAsync(newUser, request.Password);
        if (!userCreationResult.Succeeded)
            throw new UserRegistrationException("Registration error", userCreationResult.Errors);
        
        return await _userManager.FindByNameAsync(request.Username);
    }
    private Task AddIdentityClaims(IdentityUser identityUser, SignupCommand command)
    {
        var tasks = new List<Task>
        {
            _userManager.AddClaimAsync(identityUser, new Claim("Phone", command.PhoneNumber)),
            _userManager.AddClaimAsync(identityUser, new Claim("Email", command.Email))
        };

        foreach (var role in command.Roles ?? Enumerable.Empty<string>())
        {
            tasks.Add(_userManager.AddClaimAsync(identityUser, new Claim("Role", role)));
        }

        return Task.WhenAll(tasks);
    }

    
}