using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Handlers.Queries;
using TaskManagement.Queries;

namespace API.Controllers.Queries
{
    [ApiController]
    [Route("[controller]")]
    public class TaskQueryController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public TaskQueryController(IQueryDispatcher queryDispatcher)
        => _queryDispatcher = queryDispatcher;

        [HttpGet(GetAllTaskHandler.ROUTE)]
        public async Task<IActionResult> GetAllTasks([FromQuery] GetAllTaskRequest request)
        => Ok(await _queryDispatcher.QueryAsync(request).ConfigureAwait(false));

        [HttpGet(GetTaskByHandler.ROUTE)]
        public async Task<IActionResult> GetTaskById([FromQuery] GetTaskByIdRequest request)
        => Ok(await _queryDispatcher.QueryAsync(request).ConfigureAwait(false));
    }
}
