using RemoteAc.Core.Entities;

namespace RemoteAc.Core.Interfaces.Services;

public interface IAppStateService
{
    AppState GetAppState();
    bool? AcUnitOn { get; set; }
    string? WeatherStation { get; set; }
    string? WxGridPoints { get; set; }
}
