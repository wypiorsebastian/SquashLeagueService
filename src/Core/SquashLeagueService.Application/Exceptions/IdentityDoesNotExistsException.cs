namespace SquashLeagueService.Application.Exceptions;

public sealed class IdentityDoesNotExistsException : ApplicationException
{
    public IdentityDoesNotExistsException(string identityId) : base($"Identity with id {identityId} doe not exists")
    {
    }
}