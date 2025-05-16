using Microsoft.Extensions.Logging;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;
using TaskManagement.Queries;

namespace TaskManagement.Application.Handlers.Queries
{
    public record GetAllTaskRequest(Guid ProjectId) : IQuery<List<TaskItem>> { }

    public class GetAllTaskHandler : IQueryHandler<GetAllTaskRequest, List<TaskItem>>
    {
        public const string ROUTE = "Task.GetAll";
        private readonly IGetProjectById _getProjectById;
        private readonly ILogger<GetAllTaskHandler> _logger;

        public GetAllTaskHandler(ILogger<GetAllTaskHandler> logger,
                                        IGetProjectById getProjectById)
        {
            _logger = logger;
            _getProjectById = getProjectById;
        }

        public async Task<List<TaskItem>> HandleAsync(GetAllTaskRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing get all tasks");
            var project = await GetProjectById(request.ProjectId, cancellationToken);
            return project.Tasks.ToList();
        }

        private async Task<Project> GetProjectById(Guid id, CancellationToken cancellationToken)
        {
            _getProjectById.Id = id;
            var project = await _getProjectById.Query(cancellationToken);
            if (project == null)
                throw new InvalidOperationException("there is no project with same Id.");
            return project;
        }
    }
}
