using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Commands
{
    public sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            var handleAsyncMethod = handler.GetType().GetMethod("HandleAsync");
            if (handleAsyncMethod == null)
            {
                throw new InvalidOperationException($"HandleAsync method not found on type '{handler.GetType().FullName}'.");
            }

            var resultTask = (Task<TResult>)handleAsyncMethod.Invoke(handler, new object[] { command, cancellationToken });
            return await resultTask.ConfigureAwait(false);
        }
    }
}
