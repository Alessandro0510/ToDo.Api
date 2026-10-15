namespace ToDo.Api.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        //[MaxLenght(150)]
        public string Name { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
