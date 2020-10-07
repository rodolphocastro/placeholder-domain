using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Placeholder.Domain.Aggregates;
using Placeholder.Domain.Entities;
using Placeholder.Worker.Storage;

namespace Placeholder.Worker
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;

        public Worker(ILogger<Worker> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using var scope = _provider.CreateScope();
                await HandleScoped(scope.ServiceProvider.GetRequiredService<TodoAggregate>(), scope.ServiceProvider.GetRequiredService<WorkerContext>(), stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }

        protected virtual async Task HandleScoped(TodoAggregate todoAggregate, WorkerContext context, CancellationToken ct)
        {
            var countTodos = await context.Todos.CountAsync(ct);
            _logger.LogInformation("There are {countTodos} Todos in this Worker", countTodos);
            async Task CreateNewTodo()
            {
                var newTodo = new Todo { Completed = false, Title = Guid.NewGuid().ToString(), UserId = 1 };
                context.Add(newTodo);
                await context.SaveChangesAsync(ct);
            }

            await CreateNewTodo();

            async Task AnnotateRandomTodo(int maxId)
            {
                if (maxId == 0)
                {
                    return;
                }
                var randomId = new Random().Next(1, maxId);
                var subject = await context.Todos.SingleOrDefaultAsync(t => t.Id == randomId, ct);
                var anotated = await todoAggregate.CreateAnotated(subject, Guid.NewGuid().ToString(), ct);
                context.Add(anotated);
                await context.SaveChangesAsync(ct);

            }

            await AnnotateRandomTodo(countTodos);
        }
    }
}
