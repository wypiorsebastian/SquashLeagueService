using SquashLeagueService.Domain.Exceptions;

namespace SquashLeagueService.Application.Common.Exceptions;

public class UserNotFoundException : DomainExceptionBase
{
    public override string Code => "user_not_found";

    public UserNotFoundException(string userId) : base($"User with id {userId} has not been found")
    {}
}