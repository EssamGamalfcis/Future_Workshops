using TaskManagement.Domains;

namespace TaskManagement.Domain.Entities
{
    public class Project : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public virtual ICollection<TaskItem> Tasks { get; set; } = new HashSet<TaskItem>();

        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }
        public static Project Create(string name) => new(name);
        public void Update(string name)
        => Name = name;
        public Project AddTask(string title, string description, DateTime dueDate)
        {
            var task = TaskItem.Create(title, description, dueDate);
            Tasks.Add(task);
            return this;
        }
        public Project UpdateTask(Guid id, string title, string description, DateTime dueDate, TaskStatus status)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            task.Update(title, description, dueDate, status);
            return this;
        }
        public void CompleteTask(Guid taskId)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new InvalidOperationException("Task not found.");

            task.MarkAsCompleted();
        }
    }
}