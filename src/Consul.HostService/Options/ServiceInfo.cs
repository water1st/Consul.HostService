namespace Consul.HostService
{
    public class ServiceInfo
    {
        public ServiceInfo()
        {
            HealthCheck = new HealthCheckInfo();
        }

        public string ServicesName { get; set; }
        public string ServicesId { get; set; }
        public string ServiceAddress { get; set; }
        public int ServicePort { get; set; }

        public HealthCheckInfo HealthCheck { get; set; }
    }
}
