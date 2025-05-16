using Microsoft.Extensions.Logging;
using TaskManagement.BaseResponse;
using TaskManagement.Commands;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

namespace TaskManagement.Application.Handlers.Commands
{
    public record UpdateTaskRequest(Guid ProjectId, Guid Id, string Title, string Description, DateTime DueDate, Domain.Entities.TaskStatus Status) : ICommand<BaseWriteResponse> { }

    public class UpdateTaskHandler : ICommandHandler<UpdateTaskRequest, BaseWriteResponse>
    {
        public const string ROUTE = "Task.Update";
        private readonly IGetProjectById _getProjectById;
        private readonly ILogger<UpdateTaskHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public static string Created = "UPDATED_SUCCESSED";

        public UpdateTaskHandler(ILogger<UpdateTaskHandler> logger,
                                    IGetProjectById getProjectById,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _getProjectById = getProjectById;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseWriteResponse> HandleAsync(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing update Task {Title} , {Id}", request.Title, request.Id);
            var project = await GetProjectById(request.ProjectId, cancellationToken);
            project.UpdateTask(request.Id, request.Title, request.Description, request.DueDate, request.Status);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new BaseWriteResponse { Success = true, Code = Created };
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
