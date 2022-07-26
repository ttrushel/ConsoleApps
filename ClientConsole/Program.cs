using ClientConsoleApp.Callouts;
using ClientConsoleApp.Data;
using ClientConsoleApp.Helpers;
using ClientConsoleApp.Repositories;
using ClientConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClientConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var service = host.Services.GetService<IClientService>();
            var response = await service.RunAsync();
            Console.WriteLine(response);
            Console.ReadLine();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddAutoMapper(typeof(Program));
                    services.AddDbContext<ClientContext>(options =>
                          options.UseSqlServer(@"Server=localhost\\SQLEXPRESS;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true"));
                    services.AddSingleton<IClientService, ClientService>();
                    services.AddScoped<IClientCalloutService, ClientCalloutService>();
                    services.AddSingleton<IClientRepository, ClientRepository>();
                    services.AddSingleton<ILoggerManager, LoggerManager>();
                    services.AddHttpClient();
                });
            return hostBuilder;
        }
    }
}