using SquashLeagueService.Domain.Abstractions;
using SquashLeagueService.Domain.Exceptions;

namespace SquashLeagueService.Domain.Entities;

public sealed class Member : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string IdentityUserId { get; private set; }

    private Member(string firstName, string lastName, string identityUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        IdentityUserId = identityUserId;
    }

    public static Member Create(string firstName, string lastName, string identityUserId)
    {
        var member = new Member(firstName, lastName, identityUserId);
        member.ValidateMember(firstName, lastName, identityUserId);
        
        return member;
    }

    private void ValidateMember(string firstName, string lastName, string identityUserId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidFirstNameException();

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidLastNameException();
        
        if (string.IsNullOrWhiteSpace(identityUserId))
            throw new InvalidIdentityId();

        if (!Guid.TryParse(identityUserId, out var id))
            throw new InvalidIdentityId(identityUserId);
    }
    
}