using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Handlers.Commands;
using TaskManagement.Commands;

namespace API.Controllers.Commands
{
    [ApiController]
    [Route("[controller]")]
    public class TaskCommandController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public TaskCommandController(ICommandDispatcher commandDispatcher)
        => _commandDispatcher = commandDispatcher;

        [HttpPost(CreateTaskHandler.ROUTE)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
        => Ok(await _commandDispatcher.DispatchAsync(request, cancellationToken).ConfigureAwait(false));

        [HttpPost(UpdateTaskHandler.ROUTE)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
        => Ok(await _commandDispatcher.DispatchAsync(request, cancellationToken).ConfigureAwait(false));
    }
}
