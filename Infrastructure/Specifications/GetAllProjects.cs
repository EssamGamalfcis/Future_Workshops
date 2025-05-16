using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;
public class GetAllProjects : IGetAllProjects, IScopedInjectable
{
    public BaseList BaseList { get; set; }
    private readonly ReadDbContext _dbContext;
    public GetAllProjects(ReadDbContext dbContext)
    => _dbContext = dbContext;

    public async Task<List<Project>> Query(CancellationToken cancellationToken)
    {
        var query = BuildQuery();
        var sortDirection = string.IsNullOrEmpty(BaseList.Sort) ? "asc" : BaseList.Sort.ToLower();
        query = query.OrderBy($"{BaseList.OrderBy} {sortDirection}");
        query = query
            .Skip((BaseList.Page - 1) * BaseList.Limit)
            .Take(BaseList.Limit);
        return await query.ToListAsync(cancellationToken);

    }
    public async Task<int> GetTotal(CancellationToken cancellationToken)
    {
        var query = BuildQuery();
        return await query.CountAsync(cancellationToken);
    }
    private IQueryable<Project> BuildQuery()
    {
        var query = _dbContext.Projects.AsNoTracking().AsQueryable();
        return query;
    }
}
