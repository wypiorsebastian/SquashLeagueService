namespace SquashLeagueService.Domain.Exceptions;

public sealed class InvalidIdentityId : DomainExceptionBase
{
    public InvalidIdentityId() : base("Member identifier cannot be null or empty value")
    {
        Code = "invalid_identifier";
    }

    public InvalidIdentityId(string identifier) : base($"Value: '{identifier}' is not a valid identifier")
    {
        Code = "invalid_identifier";
    }
}