using System.Linq;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MetricsController(IDhtSensorDataService dhtSensorDataService) : Controller
{
    private static readonly ILogger _logger = Log.ForContext<MetricsController>();
    [HttpGet]
    [Route("humidity_avg")]
    public async Task<IActionResult> GetHumidityAvg()
    {
        _logger.Debug("GetHumidityAvg called");
        var result = (await dhtSensorDataService.GetAll())
            .Average(x => x.Humidity);
        return Ok(result);
    }
    [HttpGet]
    [Route("temperature_avg")]
    public async Task<IActionResult> GetTemperatureAvg()
    {
        _logger.Debug("GetTemperatureAvg called");
        var result = (await dhtSensorDataService.GetAll())
            .Average(x => x.TempC);
        return Ok(result);
    }
    [HttpGet]
    [Route("humidity_max")]
    public async Task<IActionResult> GetHumidityMax()
    {
        _logger.Debug("GetHumidityMax called");
        var result = (await dhtSensorDataService.GetAll())
            .Max(x => x.Humidity);
        return Ok(result);
    }
    [HttpGet]
    [Route("temperature_max")]
    public async Task<IActionResult> GetTemperatureMax()
    {
        _logger.Debug("GetTemperatureMax called");
        var result = (await dhtSensorDataService.GetAll())
            .Max(x => x.TempC);
        return Ok(result);
    }
    [HttpGet]
    [Route("humidity_min")]
    public async Task<IActionResult> GetHumidityMin()
    {
        _logger.Debug("GetHumidityMin called");
        var result = (await dhtSensorDataService.GetAll())
            .Min(x => x.Humidity);
        return Ok(result);
    }
    [HttpGet]
    [Route("temperature_min")]
    public async Task<IActionResult> GetTemperatureMin()
    {
        _logger.Debug("GetTemperatureMin called");
        var result = (await dhtSensorDataService.GetAll())
            .Min(x => x.TempC);
        return Ok(result);
    }
}
