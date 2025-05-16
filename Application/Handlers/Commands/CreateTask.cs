using Microsoft.Extensions.Logging;
using TaskManagement.BaseResponse;
using TaskManagement.Commands;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

namespace TaskManagement.Application.Handlers.Commands
{
    public record CreateTaskRequest(Guid ProjectId, string Title, string Description, DateTime DueDate) : ICommand<BaseWriteResponse> { }

    public class CreateTaskHandler : ICommandHandler<CreateTaskRequest, BaseWriteResponse>
    {
        public const string ROUTE = "Task.Create";
        private readonly IGetProjectById _getProjectById;
        private readonly IChangeTracker _changeTracker;
        private readonly ILogger<CreateTaskHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public static string Created = "CREATED_SUCCESSED";

        public CreateTaskHandler(ILogger<CreateTaskHandler> logger,
                                    IGetProjectById getProjectById,
                                    IChangeTracker changeTracker,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _getProjectById = getProjectById;
            _unitOfWork = unitOfWork;
            _changeTracker = changeTracker;
        }

        public async Task<BaseWriteResponse> HandleAsync(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing create task {Name}", request.Title);
            var project = await CheckProjectById(request.ProjectId, cancellationToken);
            project.AddTask(request.Title, request.Description, request.DueDate);
            await ChangeTracker(project.Tasks.FirstOrDefault(x => x.ProjectId == default), cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new BaseWriteResponse { Success = true, Code = Created };
        }

        private async Task ChangeTracker(TaskItem taskItem, CancellationToken cancellationToken)
        {
            _changeTracker.TaskItem = taskItem;
            await _changeTracker.Query(cancellationToken);
        }

        private async Task<Project> CheckProjectById(Guid id, CancellationToken cancellationToken)
        {
            _getProjectById.Id = id;
            var project = await _getProjectById.Query(cancellationToken);
            if (project == null)
                throw new InvalidOperationException("there are no project with same id.");
            return project;
        }
    }
}
