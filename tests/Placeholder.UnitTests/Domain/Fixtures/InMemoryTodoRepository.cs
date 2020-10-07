using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Placeholder.Domain.Entities;
using Placeholder.Domain.Repositories;

namespace Placeholder.UnitTests.Domain.Fixtures.Adapters
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        public int DeleteCalledCount { get; set; } = 0;
        public int GetCalledCount { get; set; } = 0;
        public int GetAllCalledCount { get; set; } = 0;
        public int SaveCalledCount { get; set; } = 0;

        protected ICollection<Todo> Todos { get; set; } = new HashSet<Todo>();

        public InMemoryTodoRepository() { }

        public Task Delete(int userId, int todoId, CancellationToken ct = default)
        {
            DeleteCalledCount++;

            return Task.CompletedTask;
        }

        public Task<Todo> Get(int userId, int todoId, CancellationToken ct = default)
        {
            GetCalledCount++;

            var result = Todos.SingleOrDefault(t =>
                t.UserId == userId &&
                t.Id == todoId)
                ?? Todo.Empty;

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Todo>> GetAll(CancellationToken ct = default)
        {
            GetAllCalledCount++;
            var result = Todos.AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<Todo> Save(Todo todo, CancellationToken ct = default)
        {
            SaveCalledCount++;
            Todos.Add(todo);
            return Task.FromResult(todo);
        }
    }
}
