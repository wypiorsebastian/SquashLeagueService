using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    {}

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}