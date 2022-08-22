using Microsoft.AspNetCore.Identity;
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

    public Task<ApplicationUser> GetApplicationUser(string id)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<IdentityRole>> GetUserRoles(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        var userRoles = await _context.UserRoles.Where(x => x.UserId == id).ToListAsync();
        var wholeRoles = await _context.Roles.ToListAsync();
        var roles = wholeRoles.Where(x => userRoles.Any(r => r.RoleId == x.Id));
        return roles.ToList();
    }

    public void UpdateUser(ApplicationUser user)
    {
        _context.Users.Update(user);
    }
}