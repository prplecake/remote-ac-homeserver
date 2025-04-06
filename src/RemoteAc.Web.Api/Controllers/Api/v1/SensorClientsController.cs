using System.Collections.Generic;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Services;
using RemoteAc.Web.Api.Models;

namespace RemoteAc.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SensorsController : Controller
{
    private static readonly ILogger Log = Serilog.Log.ForContext<SensorsController>();
    private readonly ISensorClientService _service;
    public SensorsController(ISensorClientService sensorClientService)
    {
        _service = sensorClientService;
    }
    [HttpGet]
    public async Task<IActionResult> GetSensor()
    {
        Log.Debug("GetSensor got GET");
        return Ok(new Response<List<SensorClient>>(await _service.GetSensors()));
    }
}
