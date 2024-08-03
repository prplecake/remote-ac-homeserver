using System.ComponentModel;
using System.Diagnostics;
using RemoteAc.Core.Interfaces;

namespace RemoteAc.Web.Api.Services;

public class RemoteControlService : IRemoteControlService
{
    private const string RemoteName = "ac-remote";
    public async Task<(bool IsSuccess, string? Error)> SendOnce(string command) => await RunCommandAsync($"SEND_ONCE {RemoteName} {command}");
    public async Task<bool> SendMany(string command, int? duration)
    {
        if (!(await RunCommandAsync($"SEND_START {RemoteName} {command}")).IsSuccess)
            return false;
        await Task.Delay(duration ?? 3 * 1000);
        if (!(await RunCommandAsync($"SEND_STOP {RemoteName} {command}")).IsSuccess)
            return false;
        return true;
    }
    private async Task<(bool IsSuccess, string? Error)> RunCommandAsync(string arguments)
    {
        Process proc = new();
        proc.StartInfo.FileName = "irsend";
        proc.StartInfo.Arguments = arguments;
        try
        {
            proc.Start();
        }
        catch (Win32Exception ex)
        {
            return (false, $"Error: {ex.Message}");
        }
        return (proc.ExitCode == 0,
            proc.ExitCode == 0 ? null : $"Error: {await proc.StandardError.ReadToEndAsync()}");
    }
}
