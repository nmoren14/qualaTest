using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QUALA.Data;

namespace QUALA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppContext.SetSwitch("Switch.System.Net.DontEnableSystemDefaultTlsVersions", false);

            var host = CreateHostBuilder(args).Build();
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var tester = new DatabaseConnectionTester(serviceProvider.GetRequiredService<IConfiguration>());
                tester.TestConnection();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddRazorPages();
                    })
                    .Configure((hostContext, app) =>
                    {
                        // Configuraciones adicionales del pipeline de solicitud HTTP
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapRazorPages();
                        });
                    });
                });        
    }
}
