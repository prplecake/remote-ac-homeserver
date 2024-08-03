using RemoteAc.Core.Entities;
using RemoteAc.Core.Filters;

namespace RemoteAc.Core.Interfaces.Services;

public interface IDhtSensorDataService
{
    Task<DhtSensorData?> GetLastRecord();
    Task<IEnumerable<DhtSensorData>> GetAll(PaginationFilter? filter = null);
    Task<int> GetTotalRecordsAsync();
    Task<IEnumerable<DhtSensorData>> GetGraphData(PaginationFilter filter);
}
