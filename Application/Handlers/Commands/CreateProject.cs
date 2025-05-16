using Microsoft.Extensions.Logging;
using TaskManagement.BaseResponse;
using TaskManagement.Commands;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

namespace TaskManagement.Application.Handlers.Commands
{
    public record CreateProjectRequest(string Name) : ICommand<BaseWriteResponse> { }

    public class CreateProjectHandler : ICommandHandler<CreateProjectRequest, BaseWriteResponse>
    {
        public const string ROUTE = "Project.Create";
        private readonly IGetProjectByName _getProjectByName;
        private readonly ILogger<CreateProjectHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public static string Created = "CREATED_SUCCESSED";

        public CreateProjectHandler(ILogger<CreateProjectHandler> logger,
                                    IGetProjectByName getProjectByName,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _getProjectByName = getProjectByName;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseWriteResponse> HandleAsync(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing create project {Name}", request.Name);
            await CheckProjectByName(request.Name, cancellationToken);
            var project = Project.Create(request.Name);
            await _unitOfWork.Add<Project, Guid>(project, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new BaseWriteResponse { Success = true, Code = Created };
        }

        private async Task CheckProjectByName(string name, CancellationToken cancellationToken)
        {
            _getProjectByName.Name = name;
            _getProjectByName.Id = null;
            var project = await _getProjectByName.Query(cancellationToken);
            if (project != null)
                throw new InvalidOperationException("there are a project with same name please choose a different name.");
        }
    }
}
