using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Specifications
{
    public interface IGetAllProjects : IAsyncSpecification<List<Project>>
    {
        public BaseList BaseList { get; set; }
        Task<int> GetTotal(CancellationToken cancellationToken);
    }
}