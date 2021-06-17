# Consul.HostService

### Getting started
To use the consul service, first install the [NuGet package](https://www.nuget.org/packages/Consul.HostService/):

```powershell
Install-Package Consul.HostService
``` 

Next, register to IServiceCollection
```csharp
            services.AddConsulHostService(builder =>
            {
                builder.Options.ConsulServers = new string[] { "http://consul-node1:8500", "http://consul-node2:8500", "http://consul-node3:8500", "http://consul-node4:8500" };
                builder.Options.Datacenter = "dc1";
                builder.Options.ServiceInfo.ServicesId = builder.GetContainerID();
                builder.Options.ServiceInfo.ServicesName = "test_service";
                builder.Options.ServiceInfo.ServiceAddress = builder.GetCurrentHost();
                builder.Options.ServiceInfo.ServicePort = 80;
                builder.Options.ServiceInfo.HealthCheck.Endpoint = $"http://{builder.Options.ServiceInfo.ServiceAddress}:{builder.Options.ServiceInfo.ServicePort}/api/health";
                builder.Options.ServiceInfo.HealthCheck.DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5);
                builder.Options.ServiceInfo.HealthCheck.Interval = TimeSpan.FromSeconds(10);
                builder.Options.ServiceInfo.HealthCheck.Timeout = TimeSpan.FromSeconds(5);
            });
```

### Run the sample
Confirm that your host was installed [Docker](https://www.docker.com/) 

Run the command
```brash
docker-compose up -d --build
```
