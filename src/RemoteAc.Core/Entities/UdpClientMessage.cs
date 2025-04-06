using System.Text.Json.Serialization;

namespace RemoteAc.Core.Entities;

public class UdpClientMessage
{
    public string Hostname { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
    [JsonPropertyName("mtype")]
    public string MessageType { get; set; }
}
