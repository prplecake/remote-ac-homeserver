using RemoteAc.Core.Entities.Base;

namespace RemoteAc.Core.Entities;

public class SensorClient : Entity
{
    public string Hostname { get; set; }
    public string Address { get; set; }
    public string Mac { get; set; }
    public int Port { get; set; }

    public static implicit operator SensorClient(UdpClientMessage message)
        => new()
        {
            Hostname = message.Hostname,
            Address = message.Address,
            Mac = message.Mac,
            Port = message.Port
        };
}
