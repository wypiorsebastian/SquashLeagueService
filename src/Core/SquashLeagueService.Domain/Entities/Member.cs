using SquashLeagueService.Domain.Abstractions;

namespace SquashLeagueService.Domain.Entities;

public class Member : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityUserId { get; set; }
    
}