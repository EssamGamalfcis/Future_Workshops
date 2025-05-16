using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Specifications
{
    public interface IGetProjectById : IAsyncSpecification<Project?>
    {
        Guid Id { get; set; }
    }
}