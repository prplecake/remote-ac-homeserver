using System.Reflection;
using Microsoft.AspNetCore.Http;
using RemoteAc.Core.Entities;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.API2.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AppStateController : Controller
{
    private static readonly ILogger _logger = Log.ForContext<AppStateController>();
    private readonly IAppStateService _appState;
    public AppStateController(IAppStateService appState)
    {
        _appState = appState;
    }
    [HttpGet]
    public async Task<IActionResult> GetAppState()
    {
        _logger.Debug("GetAppState got GET");
        return Ok(_appState);
    }
    [HttpPost]
    [Route("ac_power/toggle")]
    public async Task<IActionResult> ToggleAcPower()
    {
        _logger.Debug("ToggleAcPower got POST");
        try
        {
            _appState.AcUnitOn = !_appState.AcUnitOn;
            return Ok(_appState);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
    [HttpPatch]
    public async Task<IActionResult> PatchAppState([FromBody]AppState updatedAppState)
    {
        _logger.Debug("PatchAppState got PATCH");
        // iterate through all of the properties of the update object
        // in this example, it includes all properties apart from `id`
        foreach (PropertyInfo prop in updatedAppState.GetType().GetProperties())
        {
            // check if the property has been set in the updated object
            // if it is null we ignore it. If you want to allow null values to be set,
            // you could add a flag to the update object to allow specific nulls
            if (prop.GetValue(updatedAppState) != null)
            {
                // if it has been set, perform the updates
                _appState.GetType().GetProperty(prop.Name)?.SetValue(_appState, prop.GetValue(updatedAppState));
            }
        }
        return Ok(_appState);
    }
}
