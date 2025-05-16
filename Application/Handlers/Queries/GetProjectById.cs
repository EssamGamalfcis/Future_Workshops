using Microsoft.Extensions.Logging;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;
using TaskManagement.Queries;

namespace TaskManagement.Application.Handlers.Queries
{
    public record GetProjectByIdRequest(Guid Id) : IQuery<ProjectByIdResponse> { }

    public class GetProjectByIdHandler : IQueryHandler<GetProjectByIdRequest, ProjectByIdResponse>
    {
        public const string ROUTE = "Project.GetById";
        private readonly IGetProjectById _getProjectById;
        private readonly ILogger<GetProjectByIdHandler> _logger;

        public GetProjectByIdHandler(ILogger<GetProjectByIdHandler> logger,
                                     IGetProjectById getProjectById)
        {
            _logger = logger;
            _getProjectById = getProjectById;
        }

        public async Task<ProjectByIdResponse> HandleAsync(GetProjectByIdRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing get project by id");
            var project = await GetProjectById(request.Id, cancellationToken);
            var data = ProjectByIdResponse.Map(project);
            return data;
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
