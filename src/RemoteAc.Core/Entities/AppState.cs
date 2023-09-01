using RemoteAc.Core.Entities.Base;

namespace RemoteAc.Core.Entities;

public class AppState : Entity
{
    public bool? AcUnitOn { get; set; }
    public string? WeatherStation { get; set; }
    public string? WxGridPoints { get; set; }
}
