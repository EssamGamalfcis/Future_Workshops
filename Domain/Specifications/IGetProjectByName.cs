using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Specifications
{
    public interface IGetProjectByName : IAsyncSpecification<Project?>
    {
        string Name { get; set; }
        Guid? Id { get; set; }
    }
}