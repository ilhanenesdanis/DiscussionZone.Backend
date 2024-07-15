using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace DiscussionZone.API.IOC
{
    public static class SerilogConfiguration
    {
        public static void AddSerilogConfiguration(this IServiceCollection services, IConfiguration configuration,IHostBuilder host)
        {
            Logger log = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File("logs/log.txt")
                 .WriteTo.PostgreSQL(configuration.GetConnectionString("PostgreSQL"), "ApplicationLogs",
                        needAutoCreateTable: true)
                 .Enrich.FromLogContext()
                 .MinimumLevel.Information()
                 .WriteTo.Seq(configuration.GetSection("SeqUrl")?.Value ?? string.Empty)
                 .CreateLogger();

            host.UseSerilog(log);

        }
    }
}
