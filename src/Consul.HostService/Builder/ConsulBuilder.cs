using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Consul.HostService
{
    class ConsulBuilder : IConsulBuilder
    {
        public ConsulBuilder(ConsulOption options)
        {
            Options = options;
        }

        public ConsulOption Options { get; }

        public string GetContainerID()
        {
            var hostname = Environment.GetEnvironmentVariable("HOSTNAME", EnvironmentVariableTarget.Process);
            return hostname;
        }

        public string GetCurrentHost()
        {
            var host = NetworkInterface.GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .FirstOrDefault(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address))?.Address.ToString();

            return host;
        }

    }
}
