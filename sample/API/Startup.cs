using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddHealthChecks();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHealthChecks("/api/health");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
