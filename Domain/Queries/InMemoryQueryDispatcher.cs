using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Queries;

namespace Domain.Queries;

public sealed class InMemoryQueryDispatcher : IQueryDispatcher
{

    private readonly IServiceProvider _serviceProvider;

    public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var handleAsyncMethod = handler.GetType().GetMethod("HandleAsync");
        if (handleAsyncMethod == null)
        {
            throw new InvalidOperationException($"HandleAsync method not found on type '{handler.GetType().FullName}'.");
        }
        var resultTask = (Task<TResult>)handleAsyncMethod.Invoke(handler, new object[] { query, cancellationToken });
        return await resultTask.ConfigureAwait(false);
    }
}

