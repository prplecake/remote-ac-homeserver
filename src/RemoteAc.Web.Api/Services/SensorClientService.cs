using System.Collections.Generic;
using System.Linq;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Services;

public class SensorClientService : ISensorClientService
{
    private readonly ILogger _logger = Log.ForContext<AppStateService>();
    private readonly ISensorClientRepository _repo;
    public SensorClientService(ISensorClientRepository sensorClientRepository)
    {
        _repo = sensorClientRepository;
    }
    /// <inheritdoc />
    public async Task<List<SensorClient>> GetSensors()
    {
        List<SensorClient>? sensors;
        sensors = await _repo.GetClients();
        return sensors;
    }
    public async Task AddClient(SensorClient sensorClient)
    {
        _logger.Debug("Adding sensor client {sensorClient}", sensorClient);
        if (sensorClient is null)
        {
            _logger.Error("Sensor client is null");
            throw new ArgumentNullException(nameof(sensorClient));
        }
        if (await _repo.GetClient(sensorClient.Hostname) is not null)
        {
            _logger.Error("Sensor client with hostname {Hostname} already exists", sensorClient.Hostname);
            throw new ArgumentException("Sensor client already exists");
        }
        if ((await _repo.GetClients())
            .Any(x => x.Mac == sensorClient.Mac))
        {
            _logger.Error("Sensor client with MAC address {MacAddress} already exists", sensorClient.Mac);
            throw new ArgumentException("Sensor client already exists");
        }
        _repo.AddClient(sensorClient);
    }
}
