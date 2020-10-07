using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Placeholder.Domain.Entities;
using Placeholder.Domain.Repositories;
using Placeholder.Worker.Storage;

namespace Placeholder.Worker.Adapters
{
    internal class TodoAdapter : ITodoRepository
    {
        private readonly WorkerContext _context;

        public TodoAdapter(WorkerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task Delete(int userId, int todoId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Todo> Get(int userId, int todoId, CancellationToken ct = default)
        {
            var result = await _context.Todos.SingleOrDefaultAsync(t =>
                t.UserId == userId &&
                t.Id == todoId,
                cancellationToken: ct) ?? Todo.Empty;
            return result;
        }

        public async Task<IEnumerable<Todo>> GetAll(CancellationToken ct = default)
        {
            var result = await _context.Todos.ToListAsync(ct);
            return result;
        }

        public async Task<Todo> Save(Todo todo, CancellationToken ct = default)
        {
            var found = await Get(todo.UserId, todo.Id, ct);
            if (found == Todo.Empty)
            {
                found = todo;
            }

            // TODO: This needs futher refinement (Currently this is throwing big time)
            found.Title = todo.Title;
            found.Completed = todo.Completed;
            await _context.SaveChangesAsync(ct);
            return found;
        }
    }
}
