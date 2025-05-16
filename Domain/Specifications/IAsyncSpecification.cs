public interface IAsyncSpecification<T>
{
    Task<T> Query(CancellationToken cancellationToken);
}
