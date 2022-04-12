using Microsoft.AspNetCore.Identity;

namespace SquashLeagueService.Domain.Repositories;

public interface IIdentityRepository
{
    Task<List<IdentityRole>> GetApplicationRoles();
}