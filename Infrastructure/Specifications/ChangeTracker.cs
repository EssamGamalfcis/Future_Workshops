using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

public class ChangeTracker : IChangeTracker, IScopedInjectable
{
    public TaskItem TaskItem { get; set; }
    private readonly WriteDbContext _dbContext;
    public ChangeTracker(WriteDbContext dbContext)
    => _dbContext = dbContext;

    public async Task<TaskItem> Query(CancellationToken cancellationToken)
    {
        _dbContext.Entry(TaskItem).State = EntityState.Added;
        return null;
    }
}
