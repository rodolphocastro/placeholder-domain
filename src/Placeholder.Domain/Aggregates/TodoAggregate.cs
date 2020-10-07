using System;
using System.Threading;
using System.Threading.Tasks;

using Placeholder.Domain.Entities;
using Placeholder.Domain.Repositories;

namespace Placeholder.Domain.Aggregates
{
    public class TodoAggregate
    {
        public TodoAggregate(ITodoRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected ITodoRepository Repository { get; }

        protected async Task<AnotatedTodo> CreateAnnotatedInternal(AnotatedTodo annotated, CancellationToken ct)
        {
            await Repository.Save(annotated, ct);
            return annotated;
        }

        public Task<AnotatedTodo> CreateAnotated(Todo baseTodo, string annotation, CancellationToken ct = default)
        {
            if (baseTodo is EmptyTodo)
            {
                throw new ArgumentNullException(nameof(baseTodo), "A Todo needs to exist before it's annotated");  // TODO: (Ironic, right?) This should be a proper domain exception.
            }

            if (string.IsNullOrWhiteSpace(annotation))
            {
                throw new ArgumentNullException(nameof(annotation), "An annotation cannot be empty");
            }

            var annotated = new AnotatedTodo(baseTodo);
            annotated.Observation = annotation;

            return CreateAnnotatedInternal(annotated, ct);
        }
    }
}
