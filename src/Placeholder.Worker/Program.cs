
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Placeholder.Domain.Aggregates;
using Placeholder.Domain.Repositories;
using Placeholder.Worker.Adapters;
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
                    services.AddScoped<ITodoRepository, TodoAdapter>();
                    services.AddScoped<TodoAggregate>();
                    services.AddHostedService<Worker>();
                });
    }
}
