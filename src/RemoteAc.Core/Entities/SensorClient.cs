using System.Net;
using RemoteAc.Core.Entities.Base;

namespace RemoteAc.Core.Entities;

public class SensorClient : Entity
{
    public string Hostname { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
}
