namespace Consul.HostService
{
    public class ConsulOption
    {
        public ConsulOption()
        {
            ServiceInfo = new ServiceInfo();
        }

        public string[] ConsulServers { get; set; }
        public string Datacenter { get; set; }
        public ServiceInfo ServiceInfo { get; set; }
    }
}
