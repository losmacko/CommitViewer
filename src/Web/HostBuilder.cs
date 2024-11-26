using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommitViewer.Web;
public static class AppHostBuilder
{
    public static IHost CreateAndBuild(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, cfg) =>
        {
            cfg.AddJsonFile("appsettings.json", false, true);
            cfg.AddJsonFile($"appsetings.{hostingContext.HostingEnvironment.EnvironmentName}", true, true);
        }).ConfigureServices((hostContext, services) =>
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(hostContext.Configuration);
        }).Build();
    }
}
