using Consul;
using Microsoft.Extensions.Options;

namespace FileService.API.Consul
{
    public class ConsulBalancer
    {
        private ConsulOptions _consulOptions;

        public ConsulBalancer(IOptionsMonitor<ConsulOptions> options)
        {
            _consulOptions = options.CurrentValue;
            Console.WriteLine("update:" + _consulOptions.Port);
        }

        public AgentService ChooseService(string serviceName)
        {
            var consulClient = new ConsulClient(c => c.Address = new Uri(_consulOptions.ConsulHost!));
            var services = consulClient.Agent.Services().Result.Response;
            var targetServices = services.Where(c => c.Value.Service.Equals(serviceName)).Select(c => c.Value);
            if (targetServices.Count() == 0)
            {
                return null!;
            }
            var targetService = targetServices!.ElementAt(new Random().Next(1, 1000) % targetServices.Count());

            return targetService;
        }
    }
}
