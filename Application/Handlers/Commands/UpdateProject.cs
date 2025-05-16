using Microsoft.Extensions.Logging;
using TaskManagement.BaseResponse;
using TaskManagement.Commands;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

namespace TaskManagement.Application.Handlers.Commands
{
    public record UpdateProjectRequest(Guid Id, string Name) : ICommand<BaseWriteResponse> { }

    public class UpdateProjectHandler : ICommandHandler<UpdateProjectRequest, BaseWriteResponse>
    {
        public const string ROUTE = "Project.Update";
        private readonly IGetProjectByName _getProjectByName;
        private readonly IGetProjectById _getProjectById;
        private readonly ILogger<UpdateProjectHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public static string Created = "UPDATED_SUCCESSED";

        public UpdateProjectHandler(ILogger<UpdateProjectHandler> logger,
                                    IGetProjectByName getProjectByName,
                                    IGetProjectById getProjectById,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _getProjectByName = getProjectByName;
            _getProjectById = getProjectById;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseWriteResponse> HandleAsync(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing update project {Name} , {Id}", request.Name, request.Id);
            var project = await GetProjectById(request.Id, cancellationToken);
            await CheckProjectByName(request, cancellationToken);
            project.Update(request.Name);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new BaseWriteResponse { Success = true, Code = Created };
        }

        private async Task CheckProjectByName(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            _getProjectByName.Name = request.Name;
            _getProjectByName.Id = request.Id;
            var project = await _getProjectByName.Query(cancellationToken);
            if (project != null)
                throw new InvalidOperationException("there are a project with same name please choose a different name.");
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
