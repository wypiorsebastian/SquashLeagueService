using Microsoft.Extensions.Logging;

namespace SquashLeagueService.Persistence.Repositories.UnitOfWork;

internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");
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