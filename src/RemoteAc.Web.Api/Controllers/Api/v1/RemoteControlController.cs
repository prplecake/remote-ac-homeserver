using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using RemoteAc.Core.Interfaces;
using RemoteAc.Web.Api.Models;

namespace RemoteAc.Web.Api.Controllers.Api.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RemoteControlController : Controller
{
    private static readonly ILogger _logger = Log.ForContext<RemoteControlController>();
    private IRemoteControlService _remoteControlService;
    public RemoteControlController(IRemoteControlService remoteControlService)
    {
        _remoteControlService = remoteControlService;
    }
    [HttpPost]
    [Route("send_once")]
    public async Task<IActionResult> SendOnce([FromBody] IrCommandRequest command)
    {
        _logger.Debug("SendOnce got POST");
        if (command.Command is null)
            return BadRequest(new Response<string>("Command is required"));
        // Send the IR command
        (bool IsSuccess, string? Error) cmdResult;
        try
        {
            cmdResult = await _remoteControlService.SendOnce(GetCommand(command.Command));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new Response<string>(ex.Message));
        }
        if (cmdResult.IsSuccess)
            return Ok(new Response<string>("OK"));
        return StatusCode(StatusCodes.Status500InternalServerError, cmdResult.Error);
    }
    [HttpPost]
    [Route("send_many")]
    public async Task<IActionResult> SendMany([FromBody] IrCommandRequest command)
    {
        _logger.Debug("SendMany got POST");
        if (command.Command is null)
            return BadRequest(new Response<string>("Command is required"));
        try
        {
            // Send the IR command
            _remoteControlService.SendMany(GetCommand(command.Command), command.Duration ?? 3);
            return Ok(new Response<string>("OK"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
    private static string GetCommand(string command)
        => command switch
        {
            "POWER" => "KEY_POWER",
            "TEMP_TIMER_UP" => "TEMP_TIMER_UP",
            "TEMP_TIMER_DWN" => "TEMP_TIMER_DWN",
            "FAN_SPEED_INC" => "FAN_SPEED_INC",
            "FAN_SPEED_DEC" => "FAN_SPEED_DEC",
            "MODE_COOL" => "MODE_COOL",
            "MODE_ENERGY_SAVER" => "MODE_ENERGY_SAVER",
            "MODE_FAN_ONLY" => "MODE_FAN_ONLY",
            "MODE_SLEEP" => "MODE_SLEEP",
            "MODE_AUTO_FAN" => "MODE_AUTO_FAN",
            "MODE_TIMER" => "MODE_TIMER",
            _ => throw new InvalidOperationException($"Invalid command: {command}")
        };
}