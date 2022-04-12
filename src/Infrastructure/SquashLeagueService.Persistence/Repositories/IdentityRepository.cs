using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Persistence.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly ApplicationDbContext _context;

    public IdentityRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<IdentityRole>> GetApplicationRoles()
    {
        return  _context.Roles.ToListAsync();
    }
}