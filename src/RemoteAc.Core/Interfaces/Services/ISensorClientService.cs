using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Services;

public interface ISensorClientService
{
    Task<List<SensorClient>> GetSensors();
    Task AddClient(SensorClient sensorClient);
}
