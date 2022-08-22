namespace SquashLeagueService.Domain.Exceptions;

public class AppExceptionBase : Exception
{
    public virtual string? Code { get; }

    public AppExceptionBase() : base() {}

    public AppExceptionBase(string message) : base(message) { }
}