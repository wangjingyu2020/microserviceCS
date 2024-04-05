using Gateway.Consul;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Ocelot services
            builder.Services.AddOcelot().AddConsul();

            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ConsulOptions>(builder.Configuration.GetSection("ConsulOptions"));

            builder.Configuration.AddJsonFile("ocelot.json", false, true);


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/health", async context =>
                {
                    await context.Response.WriteAsync("Healthy");
                });
            });


            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            // Add Ocelot middleware
            app.UseOcelot().Wait();

            app.UseConsulRegistry(app.Lifetime);

            app.Run();
        }
    }
}
