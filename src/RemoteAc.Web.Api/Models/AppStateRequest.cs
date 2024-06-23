namespace RemoteAc.Web.Api.Models;

public class AppStateRequest
{
    public bool AcUnitOn { get; set; }
    public string? WeatherStation { get; set; }
    public string? WxGridPoints { get; set; }
}
