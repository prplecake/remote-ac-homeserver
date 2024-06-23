using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Services;
using RemoteAc.Web.Api.Filters;
using RemoteAc.Web.Api.Models;

namespace RemoteAc.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DhtController(IDhtSensorDataService dhtSensorDataService, IUriService uriService) : Controller
{
    private static readonly ILogger _logger = Log.ForContext<DhtController>();
    [HttpGet]
    [Route("last_record")]
    public async Task<IActionResult> GetLastRecord()
    {
        _logger.Debug("GetLastRecord called");
        var result = await dhtSensorDataService.GetLastRecord();
        if (result is null)
            return NoContent();
        return Ok(new Response<DhtSensorData>(result));
    }
    [HttpGet]
    [Route("get_data")]
    public async Task<IActionResult> GetData()
    {
        _logger.Debug("GetData called");
        var result = await dhtSensorDataService.GetAll();
        if (result is null)
            return NoContent();
        return Ok(new Response<IEnumerable<DhtSensorData>>(result));
    }
    [HttpGet]
    [Route("graph_data")]
    public async Task<IActionResult> GetGraphData([FromQuery] string? limit)
    {
        _logger.Debug("GetGraphData called");
        int limitInt = 24 * 7;
        if (limit is not null)
        {
            if (limit.EndsWith("d"))
            {
                limitInt = int.Parse(limit.TrimEnd('d')) * 24;
            }
            else if (limit.EndsWith("h"))
            {
                limitInt = int.Parse(limit.TrimEnd('h'));
            }
        }

        var filter = new PaginationFilter(1, limitInt);
        var result = await dhtSensorDataService.GetGraphData(filter);
        if (result is null)
            return NoContent();
        return Ok(new Response<IEnumerable<DhtSensorData>>(result));
    }
    [HttpGet]
    [Route("historical_data")]
    public async Task<IActionResult> GetHistoricalData([FromQuery] PaginationFilter? filter = null)
    {
        _logger.Debug("GetHistoricalData called");
        PaginationFilter validFilter;
        if (filter is null)
            validFilter = new PaginationFilter(1, 10);
        else
            validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var result = await dhtSensorDataService.GetAll(validFilter);
        if (result is null)
            return NoContent();
        return Ok(PagedResponse<IEnumerable<DhtSensorData>>
            .Create(result,
                validFilter,
                await dhtSensorDataService.GetTotalRecordsAsync(),
                uriService,
                Request.Path.Value));
    }
}
