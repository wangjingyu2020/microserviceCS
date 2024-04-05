using Autofac;
using Main.Infrastructure.service.impl;
using Main.Infrastructure.service;
using Consul;

namespace Main.Domain.IOC
{
    public class InfrastructureModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>().As<ITestService>().SingleInstance();


            builder.Register(c => new ConsulClient(consulConfig =>
            {
                // 设置Consul地址
                consulConfig.Address = new Uri("http://localhost:8500");
            })).As<IConsulClient>().SingleInstance();

        }
    }
}
