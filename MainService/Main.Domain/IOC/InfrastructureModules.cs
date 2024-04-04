using Autofac;
using Main.Infrastructure.service.impl;
using Main.Infrastructure.service;


namespace Main.Domain.IOC
{
    public class InfrastructureModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>().As<ITestService>().SingleInstance();
        }
    
    }
}
