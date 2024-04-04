using Autofac;
using Main.Domain.Services.Impl;
using Main.Domain.Services;


namespace Main.Domain.IOC
{
    public class DomainModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().As<IBookService>().SingleInstance();
        }
    }
}
