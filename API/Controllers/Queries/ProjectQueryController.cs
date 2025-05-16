using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Handlers.Queries;
using TaskManagement.Queries;

namespace API.Controllers.Queries
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectQueryController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public ProjectQueryController(IQueryDispatcher queryDispatcher)
        => _queryDispatcher = queryDispatcher;

        [HttpGet(GetAllProjectsHandler.ROUTE)]
        public async Task<IActionResult> GetAllProjects([FromQuery] GetAllProjectsRequest request)
        => Ok(await _queryDispatcher.QueryAsync(request).ConfigureAwait(false));

        [HttpGet(GetProjectByIdHandler.ROUTE)]
        public async Task<IActionResult> GetProjectById([FromQuery] Guid id)
        => Ok(await _queryDispatcher.QueryAsync(new GetProjectByIdRequest(id)).ConfigureAwait(false));
    }
}
