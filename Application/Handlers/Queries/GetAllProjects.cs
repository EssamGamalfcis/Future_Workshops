using Microsoft.Extensions.Logging;
using TaskManagement.BaseResponse;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Helper;
using TaskManagement.Domain.Specifications;
using TaskManagement.Queries;

namespace TaskManagement.Application.Handlers.Queries
{
    public record GetAllProjectsRequest() : BaseList, IQuery<BaseReadResponse> { }

    public class GetAllProjectsHandler : IQueryHandler<GetAllProjectsRequest, BaseReadResponse>
    {
        public const string ROUTE = "Project.GetAll";
        private readonly IGetAllProjects _getAllProjects;
        private readonly ILogger<GetAllProjectsHandler> _logger;

        public GetAllProjectsHandler(ILogger<GetAllProjectsHandler> logger,
                                     IGetAllProjects getAllProjects)
        {
            _logger = logger;
            _getAllProjects = getAllProjects;
        }

        public async Task<BaseReadResponse> HandleAsync(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started processing get all projects");
            var projects = await GetAll(request, cancellationToken);
            var total = await _getAllProjects.GetTotal(cancellationToken);
            var data = ProjectResponse.Map(projects);
            return new BaseReadResponse { Items = data, Meta = new Meta { CurrentPage = request.Page, ItemsPerPage = request.Limit, ItemCount = data.Count, TotalPages = PaginationHelper.CalculateTotalPages(total, request.Limit), TotalItems = total } };
        }

        private async Task<List<Project>> GetAll(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            _getAllProjects.BaseList = new BaseList(request.Limit, request.Page, request.OrderBy, request.Sort);
            var projects = await _getAllProjects.Query(cancellationToken);
            if (projects == null || !projects.Any())
                throw new InvalidOperationException("there are no Projects");
            return projects;
        }
    }
}
