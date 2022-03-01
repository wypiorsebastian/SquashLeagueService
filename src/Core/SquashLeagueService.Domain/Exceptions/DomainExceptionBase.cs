namespace SquashLeagueService.Domain.Exceptions;

public class DomainExceptionBase : Exception
{
    public virtual string? Code { get; }

    public DomainExceptionBase() : base() {}

    public DomainExceptionBase(string message) : base(message) { }
}