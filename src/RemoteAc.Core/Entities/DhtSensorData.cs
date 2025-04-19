using System.Text.Json.Serialization;
using RemoteAc.Core.Entities.Base;

namespace RemoteAc.Core.Entities;

public class DhtSensorData : Entity
{
    [JsonPropertyName("timestamp")] public DateTime Date { get; set; }
    public double Humidity { get; set; }
    [JsonPropertyName("temperature")] public double TempC { get; set; }
}
