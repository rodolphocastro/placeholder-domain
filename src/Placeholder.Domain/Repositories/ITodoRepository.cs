using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Placeholder.Domain.Entities;

namespace Placeholder.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll(CancellationToken ct = default);

        Task<Todo> Get(int userId, int todoId, CancellationToken ct = default);

        Task<Todo> Save(Todo todo, CancellationToken ct = default);

        Task Delete(int userId, int todoId, CancellationToken = default);
    }
}
