using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Specifications
{
    public interface IChangeTracker : IAsyncSpecification<TaskItem>
    {
        public TaskItem TaskItem { get; set; }
    }
}