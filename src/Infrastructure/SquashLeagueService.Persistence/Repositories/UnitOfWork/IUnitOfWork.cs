namespace SquashLeagueService.Persistence.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    bool HasChanges { get; }
    Task CompleteAsync();
}