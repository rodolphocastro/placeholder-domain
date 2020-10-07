namespace Placeholder.Domain.Entities
{
    public class Todo
    {
        public int UserId { get; set; } = 0;

        public int Id { get; set; } = 0;

        public string Title { get; set; } = string.Empty;

        public bool Completed { get; set; } = false;

        public static Todo Empty { get; } = new EmptyTodo();
    }

    class EmptyTodo : Todo
    {
        private const int _unidentifiedId = -1;
        private const string _unidentifiedKey = "Empty";

        public EmptyTodo()
        {
            UserId = _unidentifiedId;
            Id = _unidentifiedId;
            Title = _unidentifiedKey;
            Completed = false;
        }
    }

    public class AnotatedTodo : Todo
    {
        public string Observation { get; set; } = string.Empty;
    }
}
