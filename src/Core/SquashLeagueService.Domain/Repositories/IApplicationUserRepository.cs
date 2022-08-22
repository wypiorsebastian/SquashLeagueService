using Microsoft.AspNetCore.Identity;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Domain.Repositories;

public interface IApplicationUserRepository
{
    Task<List<ApplicationUser>> GetApplicationUsers();
    Task<ApplicationUser> GetApplicationUser(string id);
    Task<List<IdentityRole>> GetUserRoles(string id);
    void UpdateUser(ApplicationUser user);
}