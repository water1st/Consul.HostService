using System;

namespace Consul.HostService
{
    public class HealthCheckInfo
    {
        public string Endpoint { get; set; }

        public TimeSpan DeregisterCriticalServiceAfter { get; set; }
        public TimeSpan Interval { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
