using System.Collections.Generic;
using System.Linq;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Core.Interfaces.Services;
using RemoteAc.Web.Api.Filters;

namespace RemoteAc.Web.Api.Services;

public class DhtSensorDataService : IDhtSensorDataService
{
    private static readonly ILogger _logger = Log.ForContext<DhtSensorDataService>();
    private readonly IDhtSensorDataRepository _repo;
    public DhtSensorDataService(IDhtSensorDataRepository dhtSensorDataRepository)
    {
        _repo = dhtSensorDataRepository;
    }
    /// <inheritdoc />
    public async Task<DhtSensorData?> GetLastRecord()
    {
        _logger.Debug("GetLastRecord called");
        return await _repo.GetLatestRecord();
    }
    /// <inheritdoc />
    public async Task<IEnumerable<DhtSensorData>> GetAll(PaginationFilter? filter)
    {
        _logger.Debug("GetAll called");
        return await _repo.GetAll(filter);
    }
    public Task<int> GetTotalRecordsAsync() => _repo.GetTotalRecordsAsync();
    public async Task<IEnumerable<DhtSensorData>> GetGraphData(PaginationFilter filter)
        => await _repo.GetGraphData(filter);
}
