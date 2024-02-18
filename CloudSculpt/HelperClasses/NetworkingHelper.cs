using System;
using System.Net;
using System.Net.NetworkInformation;
using CloudSculpt.Models;

namespace CloudSculpt.HelperClasses;

public static class NetworkingHelper
{
    public static NetworkData GetNetworkData()
    {
        var networkData = new NetworkData();
        
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.OperationalStatus != OperationalStatus.Up) continue;
            foreach (var gatewayAddr in ni.GetIPProperties().GatewayAddresses)
            {
                if (gatewayAddr.Address.ToString() == "0.0.0.0") continue;
                networkData.InterfaceName = ni.Name;
                
                foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                {
                    networkData.GatewayAddress = gatewayAddr.Address;
                    
                    if (ip.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;
                    networkData.IpAddress = ip.Address;
                
                    networkData.NetworkAddress = GetNetworkAddress(ip.Address, ip.IPv4Mask);
                    networkData.SubnetMask = ip.IPv4Mask;
                
                    networkData.Cidr = GetCidr(ip.IPv4Mask);
                    networkData.Network = $"{networkData.NetworkAddress}/{networkData.Cidr}";

                    networkData.LastFiftyIps = GetLast50IpAddresses(networkData.NetworkAddress, networkData.Cidr);
                }

                return networkData;
            }
        }

        return networkData;
    }


    private static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
    {
        var addressBytes = address.GetAddressBytes();
        var subnetMaskBytes = subnetMask.GetAddressBytes();

        if (addressBytes.Length != subnetMaskBytes.Length)
            throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

        var broadcastAddress = new byte[addressBytes.Length];
        for (var i = 0; i < broadcastAddress.Length; i++)
        {
            broadcastAddress[i] = (byte)(addressBytes[i] & subnetMaskBytes[i]);
        }
        return new IPAddress(broadcastAddress);
    }

    private static int GetCidr(IPAddress subnetMask)
    {
        var subnetMaskBytes = subnetMask.GetAddressBytes();
        var cidr = 0;
        foreach (var t in subnetMaskBytes)
        {
            var b = t;
            while (b > 0)
            {
                if ((b & 1) != 0)
                    cidr++;
                b >>= 1;
            }
        }
        return cidr;
    }
    
    private static string GetLast50IpAddresses(IPAddress networkAddress, int cidr)
    {
        var parts = networkAddress.ToString().Split('.');
        var lastOctet = int.Parse(parts[3]);
        var startingOctet = lastOctet + 256 - 50;

        return $"{parts[0]}.{parts[1]}.{parts[2]}.{startingOctet}/{cidr}";
    }
}