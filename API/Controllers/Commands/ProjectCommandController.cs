using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Handlers.Commands;
using TaskManagement.Commands;

namespace API.Controllers.Commands
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectCommandController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public ProjectCommandController(ICommandDispatcher commandDispatcher)
        => _commandDispatcher = commandDispatcher;

        [HttpPost(CreateProjectHandler.ROUTE)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
        => Ok(await _commandDispatcher.DispatchAsync(request, cancellationToken).ConfigureAwait(false));

        [HttpPost(UpdateProjectHandler.ROUTE)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
        => Ok(await _commandDispatcher.DispatchAsync(request, cancellationToken).ConfigureAwait(false));
    }
}
