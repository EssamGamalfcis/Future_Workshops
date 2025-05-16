using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

public class GetProjectById : IGetProjectById, IScopedInjectable
{
    public Guid Id { get; set; }
    private readonly WriteDbContext _dbContext;
    public GetProjectById(WriteDbContext dbContext)
    => _dbContext = dbContext;

    public async Task<Project?> Query(CancellationToken cancellationToken)
    => await _dbContext.Projects.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
}
