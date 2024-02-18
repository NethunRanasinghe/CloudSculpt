using System.Net;

namespace CloudSculpt.Models;

public class NetworkData
{
    public string InterfaceName { get; set; }
    public IPAddress IpAddress { get; set; }
    public IPAddress NetworkAddress { get; set; }
    public IPAddress SubnetMask { get; set; }
    public int Cidr { get; set; }
    public string Network { get; set; }
    public IPAddress GatewayAddress { get; set; }
    public string LastFiftyIps { get; set; }
}