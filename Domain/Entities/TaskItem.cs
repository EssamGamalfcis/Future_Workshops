using TaskManagement.Domains;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem : Entity<Guid>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskStatus Status { get; private set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        protected TaskItem() { }
        public TaskItem(string title, string description, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = TaskStatus.Pending;
        }
        public static TaskItem Create(string title, string description, DateTime dueDate) => new(title, description, dueDate);
        public void MarkAsCompleted()
        => Status = TaskStatus.Completed;

        public void Update(string title, string description, DateTime dueDate, TaskStatus status)
        {
            Title = title;
            Description = description;
            dueDate = dueDate;
            Status = status;
        }
    }

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
}