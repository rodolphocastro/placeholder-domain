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
        private static TodoAggregate CreateAggregate() => new TodoAggregate(new InMemoryTodoRepository());

        [Fact]
        public async Task Annotate_EmptyTodo_ThrowsArgumentNulLExcetpion()
        {
            // Arrange
            var subject = CreateAggregate();

            // Act
            Func<Task> act = () => subject.CreateAnotated(Todo.Empty, "A crucial observation");

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact]
        public async Task Annotate_ValidTodo_ReturnsAnotatedTodo()
        {
            // Arrange
            const string myObservation = "Witsy observation";

            var subject = CreateAggregate();
            var todo = new Todo
            {
                Id = 1,
                UserId = 1,
                Completed = true,
                Title = "My Awesome Todo"
            };

            // Act
            var result = await subject.CreateAnotated(todo, myObservation);

            // Assert
            Assert.NotEqual(Todo.Empty, result);
            Assert.Equal(myObservation, result.Observation);
        }
    }
}
