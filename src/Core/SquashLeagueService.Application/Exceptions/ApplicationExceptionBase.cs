namespace SquashLeagueService.Application.Exceptions;

public abstract class ApplicationExceptionBase : Exception
{
    public virtual string Code { get; protected set; }

    protected ApplicationExceptionBase() : base() {}

    protected ApplicationExceptionBase(string message) : base(message) { }
}