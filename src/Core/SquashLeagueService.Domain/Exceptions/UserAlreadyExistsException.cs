namespace SquashLeagueService.Domain.Exceptions;

public class UserAlreadyExistsException : DomainExceptionBase
{
    public override string Code => "user_already_exists";

    public UserAlreadyExistsException(string username) : base($"Username: {username} is already registered")
    {}

}