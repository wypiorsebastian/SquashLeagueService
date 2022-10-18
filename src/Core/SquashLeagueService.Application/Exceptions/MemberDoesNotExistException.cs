namespace SquashLeagueService.Application.Exceptions;

public sealed class MemberDoesNotExistException : ApplicationExceptionBase
{
    public MemberDoesNotExistException(string memberId) : base($"Member with id {memberId} doe not exist")
        => Code = "member_does_not_exist";
}