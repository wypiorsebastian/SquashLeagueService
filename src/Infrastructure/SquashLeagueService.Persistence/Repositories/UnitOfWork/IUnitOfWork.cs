using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Persistence.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IIdentityRepository IdentityRepository { get; }
    bool HasChanges { get; }
    Task CompleteAsync();
}