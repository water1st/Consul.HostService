using Consul.HostService;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConsulHostServiceConfigurationExtensions
    {
        public static IServiceCollection AddConsulHostService(this IServiceCollection services, Action<IConsulBuilder> config)
        {

            services.Configure<ConsulOption>(options =>
            {
                var builder = new ConsulBuilder(options);
                config(builder);
            });

            services.AddHostedService<ConsulHostService>();

            return services;
        }
    }
}
