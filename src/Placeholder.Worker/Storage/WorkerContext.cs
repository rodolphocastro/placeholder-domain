
using Microsoft.EntityFrameworkCore;

using Placeholder.Domain.Entities;

namespace Placeholder.Worker.Storage
{
    internal class WorkerContext : DbContext
    {        
        public WorkerContext(DbContextOptions options) : base(options)
        {
        }

        internal virtual DbSet<Todo> Todos { get; set; }
    }
}
