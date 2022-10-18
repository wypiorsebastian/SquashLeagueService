namespace SquashLeagueService.Domain.Exceptions;

public abstract class DomainExceptionBase : Exception
{
    public virtual string Code { get; protected set; }

    public DomainExceptionBase() : base() {}

    public DomainExceptionBase(string message) : base(message) { }
}