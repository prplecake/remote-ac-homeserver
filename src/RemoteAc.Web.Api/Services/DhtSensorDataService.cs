using System.Collections.Generic;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Core.Interfaces.Services;

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
    public async Task<IEnumerable<DhtSensorData>> GetAll()
    {
        _logger.Debug("GetAll called");
        return await _repo.GetAll();
    }
}
