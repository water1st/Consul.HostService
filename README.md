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
                var host = builder.GetCurrentHost();
                var port = 80;

                builder.Options.ConsulServers = new string[] { "http://consul1:8500", "http://consul2:8500", "http://consul3:8500", "http://consul4:8500" };
                builder.Options.Datacenter = "dc1";
                builder.Options.ServiceInfo.ServicesId = builder.GetContainerID();
                builder.Options.ServiceInfo.ServicesName = "test_service";
                builder.Options.ServiceInfo.ServiceAddress = host;
                builder.Options.ServiceInfo.ServicePort = port;
                builder.Options.ServiceInfo.HealthCheck.Endpoint = $"http://{host}:{port}/api/health";
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
