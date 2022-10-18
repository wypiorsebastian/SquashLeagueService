using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace SquashLeagueService.Application.Contracts.Identity;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateToken(IdentityUser applicationUser);
}