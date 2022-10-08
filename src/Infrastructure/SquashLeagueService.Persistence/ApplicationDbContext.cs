using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquashLeagueService.Domain.Entities;

namespace SquashLeagueService.Persistence;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Member> Members { get; set; }
    public ApplicationDbContext()
    {}

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}