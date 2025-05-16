using TaskManagement.Domains;

public interface IUnitOfWork : IDisposable
{
    Task Add<TAggregate, TKey>(TAggregate entity, CancellationToken cancellationToken)
        where TAggregate : AggregateRoot<TKey>
        where TKey : IEquatable<TKey>;

    Task Update<TAggregate>(TAggregate entity, CancellationToken cancellationToken)
        where TAggregate : class;

    Task<int> CommitAsync(CancellationToken cancellationToken);

}
