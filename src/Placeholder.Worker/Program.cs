
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Placeholder.Worker.Storage;

namespace Placeholder.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;
                    services.AddDbContext<WorkerContext>(stp =>
                        stp.UseSqlite(config.GetConnectionString(nameof(WorkerContext)))
                    );
                    services.AddHostedService<Worker>();
                });
    }
}
