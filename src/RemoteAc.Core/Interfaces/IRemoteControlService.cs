namespace RemoteAc.Core.Interfaces;

public interface IRemoteControlService
{
    Task<(bool IsSuccess, string? Error)> SendOnce(string command);
    Task<bool> SendMany(string command, int? duration);
}
