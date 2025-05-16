using Microsoft.Extensions.Logging;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;
using TaskManagement.Queries;

namespace TaskManagement.Application.Handlers.Queries
{
    public record GetTaskByIdRequest(Guid ProjectId, Guid Id) : IQuery<TaskItem> { }

    public class GetTaskByHandler : IQueryHandler<GetTaskByIdRequest, TaskItem>
    {
        public const string ROUTE = "Task.GetById";
        private readonly IGetProjectById _getProjectById;
        private readonly ILogger<GetTaskByHandler> _logger;

        public GetTaskByHandler(ILogger<GetTaskByHandler> logger,
                                        IGetProjectById getProjectById)
        {
            _logger = logger;
            _getProjectById = getProjectById;
        }

        public async Task<TaskItem> HandleAsync(GetTaskByIdRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing get task by id");
            var project = await GetProjectById(request.ProjectId, cancellationToken);
            return project.Tasks.FirstOrDefault(x => x.Id == request.Id);
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
