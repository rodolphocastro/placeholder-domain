using System;
using System.Threading.Tasks;

using Placeholder.Domain.Aggregates;
using Placeholder.Domain.Entities;
using Placeholder.UnitTests.Domain.Fixtures.Adapters;

using Xunit;

namespace Placeholder.UnitTests.Domain.Aggregates
{
    public class TodoAggregateTests
    {
        [Fact]
        public async Task Annotate_EmptyTodo_ThrowsArgumentNulLExcetpion()
        {
            // Arrange
            var subject = new TodoAggregate(new InMemoryTodoRepository());

            // Act
            Func<Task> act = () => subject.CreateAnotated(Todo.Empty, "A crucial observation");

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }
}
