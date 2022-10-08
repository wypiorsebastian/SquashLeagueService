using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SquashLeagueService.Infrastructure.Models.Settings;

namespace SquashLeagueService.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<JwtSecurityToken> GenerateToken(IdentityUser applicationUser)
    {
        var userClaims = await _userManager.GetClaimsAsync(applicationUser);
        var roles = await _userManager.GetRolesAsync(applicationUser);
        var roleClaims = new List<Claim>();
        roles.ToList().ForEach(role => roleClaims.Add(new Claim("roles", role)));

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim("uid", applicationUser.Id)
            }
            .Union(userClaims);
            //.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(_jwtSettings.TokenLifeTime),
            signingCredentials: signingCredentials
        );

        return token;
    }
}