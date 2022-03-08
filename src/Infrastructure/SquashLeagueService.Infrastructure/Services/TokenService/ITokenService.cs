using System.IdentityModel.Tokens.Jwt;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Infrastructure.Services.TokenService;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateToken(ApplicationUser applicationUser);
}