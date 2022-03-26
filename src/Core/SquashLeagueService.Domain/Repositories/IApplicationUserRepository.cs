using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Domain.Repositories;

public interface IApplicationUserRepository
{
    Task<List<ApplicationUser>> GetApplicationUsers();
}