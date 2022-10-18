namespace SquashLeagueService.Domain.Exceptions;

public sealed class InvalidFirstNameException : DomainExceptionBase
{
    public InvalidFirstNameException() : base("First name cannot be empty")
    {
        Code = "invalid_first_name";
    }
}