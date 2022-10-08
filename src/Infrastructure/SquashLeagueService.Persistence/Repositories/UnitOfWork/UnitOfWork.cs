using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using SquashLeagueService.Domain.Repositories;
using SquashLeagueService.Persistence.Repositories;

namespace SquashLeagueService.Persistence.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public IIdentityRepository IdentityRepository { get; }

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        IdentityRepository = new IdentityRepository(_context);
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }

    public bool HasChanges => _context.ChangeTracker.HasChanges();
}