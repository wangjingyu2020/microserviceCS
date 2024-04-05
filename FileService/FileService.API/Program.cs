using Autofac.Extensions.DependencyInjection;
using Autofac;
using File.Domain.IOC;
using FileService.API.Consul;

namespace MainService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<TestDbContext>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // Add services to the container.
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacModules());
                builder.RegisterModule(new DomainModules());
                builder.RegisterModule(new InfrastructureModules());
            });

            builder.Services.Configure<ConsulOptions>(builder.Configuration.GetSection("ConsulOptions"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseConsulRegistry(app.Lifetime);

            app.MapGet("/api/health", () =>
            {
                return new
                {
                    Message = "OK"
                };
            });

            app.Run();
        }
    }
}