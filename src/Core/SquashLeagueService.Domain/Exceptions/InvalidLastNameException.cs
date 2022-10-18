namespace SquashLeagueService.Domain.Exceptions;

public sealed class InvalidLastNameException : DomainExceptionBase
{
    public InvalidLastNameException() : base("Last name cannot be empty")
    {
        Code = "invalid_last_name";
    }
}