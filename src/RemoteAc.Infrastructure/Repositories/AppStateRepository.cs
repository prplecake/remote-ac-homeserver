using Microsoft.EntityFrameworkCore;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Infrastructure.Context;
using Serilog;

namespace RemoteAc.Infrastructure.Repositories;

public class AppStateRepository : IAppStateRepository
{
    private static readonly ILogger _logger = Log.ForContext<AppStateRepository>();
    private readonly RemoteAcContext _context;
    public AppStateRepository(RemoteAcContext context) => _context = context;
    /// <inheritdoc />
    public async Task<AppState> GetAppState() => await _context.AppState.FirstOrDefaultAsync();
    /// <inheritdoc />
    public AppState? SetAppState(AppState updatedAppState)
    {
        _logger.Debug("SetAppState called");
        try
        {
            var state = _context.AppState.Update(updatedAppState).Entity;
            _context.SaveChanges();
            return state;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Could not update AppState");
        }
        return null;
    }
    /// <inheritdoc />
    public void AddAppState(AppState newAppState)
    {
        _logger.Debug("AddAppState called");
        _context.AppState.Add(newAppState);
        _context.SaveChanges();
    }
}
