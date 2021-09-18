﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consul.HostService
{
    class ConsulHostService : IHostedService
    {
        private readonly ConsulOption options;
        private Lazy<string> host;

        public ConsulHostService(IOptions<ConsulOption> options)
        {
            this.options = options.Value;
            host = new Lazy<string>(() =>
            {
                var random = new Random();
                var index = random.Next(0, this.options.ConsulServers.Length);
                var host = this.options.ConsulServers[index];
                return host; ;
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var client = new ConsulClient(ConsulConfig))
            {
                AgentServiceRegistration registration = new AgentServiceRegistration();
                registration.Address = options.ServiceInfo.ServiceAddress;
                registration.Port = options.ServiceInfo.ServicePort;
                registration.ID = options.ServiceInfo.ServicesId;
                registration.Name = options.ServiceInfo.ServicesName;
                registration.Check = new AgentServiceCheck
                {
                    HTTP = options.ServiceInfo.HealthCheck.Endpoint,
                    DeregisterCriticalServiceAfter = options.ServiceInfo.HealthCheck.DeregisterCriticalServiceAfter,
                    Interval = options.ServiceInfo.HealthCheck.Interval,
                    Timeout = options.ServiceInfo.HealthCheck.Timeout,
                };

                await client.Agent.ServiceRegister(registration, cancellationToken);

            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using (var client = new ConsulClient(ConsulConfig))
            {
                await client.Agent.ServiceDeregister(options.ServiceInfo.ServicesId);
            }
        }

        private void ConsulConfig(ConsulClientConfiguration config)
        {
            var host = this.host.Value;
            config.Address = new Uri(host);
            config.Datacenter = options.Datacenter;
        }
    }
}
