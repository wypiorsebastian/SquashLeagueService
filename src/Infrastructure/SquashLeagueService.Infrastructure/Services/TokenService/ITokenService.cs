using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace SquashLeagueService.Infrastructure.Services.TokenService;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateToken(IdentityUser applicationUser);
}