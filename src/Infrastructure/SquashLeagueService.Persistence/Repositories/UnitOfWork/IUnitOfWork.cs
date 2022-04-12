using SquashLeagueService.Domain.Repositories;

namespace SquashLeagueService.Persistence.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IApplicationUserRepository Users { get; }
    IIdentityRepository Identities { get; }

    Task CompleteAsync();
}