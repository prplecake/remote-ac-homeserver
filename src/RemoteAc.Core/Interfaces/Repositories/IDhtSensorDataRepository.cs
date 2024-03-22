using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Repositories;

public interface IDhtSensorDataRepository
{
    Task<DhtSensorData?> GetLatestRecord();
    Task<IEnumerable<DhtSensorData>> GetAll();
}
