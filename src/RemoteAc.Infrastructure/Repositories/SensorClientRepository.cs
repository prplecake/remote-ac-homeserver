using Microsoft.EntityFrameworkCore;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Infrastructure.Context;
using Serilog;

namespace RemoteAc.Infrastructure.Repositories;

public class SensorClientRepository : ISensorClientRepository
{
    private static readonly ILogger _logger = Log.ForContext<SensorClientRepository>();
    private readonly RemoteAcContext _context;
    public SensorClientRepository(RemoteAcContext context) => _context = context;
    /// <inheritdoc />
    public async Task<List<SensorClient>> GetClients() => await _context.SensorClients.ToListAsync();
    /// <inheritdoc />
    public async Task<SensorClient?> GetClient(string hostname)
    {
        _logger.Debug("GetClient called");
        var client = await _context.SensorClients
            .FirstOrDefaultAsync(x => x.Hostname == hostname);
        return client;
    }
    /// <inheritdoc />
    public async Task<SensorClient?> GetClient(int id)
    {
        _logger.Debug("GetClient called");
        var client = await _context.SensorClients
            .FirstOrDefaultAsync(x => x.Id == id);
        return client;
    }
    /// <inheritdoc />
    public void AddClient(SensorClient client)
    {
        _logger.Debug("AddClient called");
        _context.SensorClients.Add(client);
        _context.SaveChanges();
    }
    /// <inheritdoc />
    public void RemoveClient(SensorClient client)
    {
        _logger.Debug("RemoveClient called");
        _context.SensorClients.Remove(client);
        _context.SaveChanges();
    }
}
