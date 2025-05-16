using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domains;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : WriteDbContext
{
    public readonly WriteDbContext _context;

    public UnitOfWork(WriteDbContext context)
    => _context = context;
    public async Task Add<TAggregate, TKey>(TAggregate entity, CancellationToken cancellationToken)
        where TAggregate : AggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        await _context.Set<TAggregate>().AddAsync(entity, cancellationToken);
    }

    public async Task Update<TAggregate>(TAggregate entity, CancellationToken cancellationToken)
        where TAggregate : class
    {
        _context.Set<TAggregate>().Update(entity);
        await Task.CompletedTask;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        var res = await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw e;
            }
        });
        return res;
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
