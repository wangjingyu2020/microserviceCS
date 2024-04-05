using Consul;
using log4net;
using Microsoft.Extensions.Options;

namespace Gateway.Consul
{
    public static class ConsulRegistryExtensions
    {
        public static WebApplication UseConsulRegistry(this WebApplication webApplication, IHostApplicationLifetime lifetime)
        {
            // To obtain the log4net logger object.
            var logger = LogManager.GetLogger(typeof(ConsulRegistryExtensions));
            var optionMonitor = webApplication.Services.GetService<IOptionsMonitor<ConsulOptions>>();
            var consulOptions = optionMonitor!.CurrentValue;
            // To retrieve the IP and Port for mindset detection.
            var ip = webApplication.Configuration["ip"] ?? consulOptions.IP;
            var port = webApplication.Configuration["port"] ?? consulOptions.Port;
            // generate serviceId
            var serviceId = Guid.NewGuid().ToString();
            logger!.InfoFormat("ip={0},port={1}", ip, port);
            // Create a Consul client object.
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(consulOptions!.ConsulHost!);
                c.Datacenter = consulOptions.ConsulDataCenter;
            });
            // Register the service with Consul.
            consulClient.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = consulOptions.ServiceName,
                Address = ip,
                Port = Convert.ToInt32(port),
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),
                    HTTP = $"http://{ip}:{port}/api/health",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                }
            });
            logger!.InfoFormat("status message is: {0}-{1}", ip, port);

            // Deregister the instance.
            lifetime.ApplicationStopped.Register(async () =>
            {
                logger!.Info("service logout");
                await consulClient.Agent.ServiceDeregister(serviceId);
            });

            return webApplication;
        }
    }
}
