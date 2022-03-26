using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Persistence.Repositories;

public class ApplicationUsersRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationUsersRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<ApplicationUser>> GetApplicationUsers()
    {
        return _context.Users.ToListAsync();
    }
}