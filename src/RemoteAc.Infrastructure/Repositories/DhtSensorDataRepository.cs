using Microsoft.EntityFrameworkCore;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Filters;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Infrastructure.Context;
using Serilog;

namespace RemoteAc.Infrastructure.Repositories;

public class DhtSensorDataRepository : IDhtSensorDataRepository
{
    private static readonly ILogger _logger = Log.ForContext<DhtSensorDataRepository>();
    private readonly RemoteAcContext _context;
    public DhtSensorDataRepository(RemoteAcContext context)
    {
        _context = context;
    }
    /// <inheritdoc/>
    public async Task<DhtSensorData?> GetLatestRecord()
    {
        _logger.Debug("GetLatestRecord called");
        try
        {
            return await _context.DhtSensorData.OrderByDescending(d => d.Date).FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("Sequence contains no elements"))
            {
                _logger.Warning("Did not get any results from database");
                return null;
            }
            _logger.Error(ex, "Did not receive any elements from database");
        }
        return null;
    }
    /// <inheritdoc />
    public async Task<IEnumerable<DhtSensorData>> GetAll(PaginationFilter? filter)
    {
        _logger.Debug("GetAll called");
        if (filter is null)
            return await _context.DhtSensorData
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        return await _context.DhtSensorData
            .OrderByDescending(d => d.Date)
            .Skip((validFilter.PageNumber -1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
    }
    public Task<int> GetTotalRecordsAsync() => _context.DhtSensorData.CountAsync();
    public async Task<IEnumerable<DhtSensorData>> GetGraphData(PaginationFilter filter)
    {
        _logger.Debug("GetGraphData called");
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        return await _context.DhtSensorData
            .OrderByDescending(d => d.Date)
            .Where(d => d.Date.Minute == 0)
            .Take(validFilter.PageSize)
            .ToListAsync();
    }
}
