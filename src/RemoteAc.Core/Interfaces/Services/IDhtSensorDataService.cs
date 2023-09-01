using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Services;

public interface IDhtSensorDataService
{
    Task<DhtSensorData?> GetLastRecord();
    Task<IEnumerable<DhtSensorData>> GetAll();
}
