using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DhtController : Controller
{
    private static readonly ILogger _logger = Log.ForContext<DhtController>();
    private readonly IDhtSensorDataService _dhtSensorData;
    public DhtController(IDhtSensorDataService dhtSensorDataService)
    {
        _dhtSensorData = dhtSensorDataService;
    }
    [HttpGet]
    [Route("last_record")]
    public async Task<IActionResult> GetLastRecord()
    {
        _logger.Debug("GetLastRecord called");
        var result = await _dhtSensorData.GetLastRecord();
        if (result is null)
            return NoContent();
        return Ok(result);
    }
    [HttpGet]
    [Route("get_data")]
    public async Task<IActionResult> GetData()
    {
        _logger.Debug("GetData called");
        var result = await _dhtSensorData.GetAll();
        if (result is null)
            return NoContent();
        return Ok(result);
    }
}
