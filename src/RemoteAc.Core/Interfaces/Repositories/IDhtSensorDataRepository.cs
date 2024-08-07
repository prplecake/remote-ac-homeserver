﻿using RemoteAc.Core.Entities;
using RemoteAc.Core.Filters;

namespace RemoteAc.Core.Interfaces.Repositories;

public interface IDhtSensorDataRepository
{
    Task<DhtSensorData?> GetLatestRecord();
    Task<IEnumerable<DhtSensorData>> GetAll(PaginationFilter? filter);
    Task<int> GetTotalRecordsAsync();
    Task<IEnumerable<DhtSensorData>> GetGraphData(PaginationFilter filter);
}
