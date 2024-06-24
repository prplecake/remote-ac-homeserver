using RemoteAc.Core.Entities.Base;

namespace RemoteAc.Core.Entities;

public class DhtSensorData : Entity
{
    public DateTime Date { get; set; }
    public double Humidity { get; set; }
    public double TempC { get; set; }
}
