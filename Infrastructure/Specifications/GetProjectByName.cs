using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Specifications;

public class GetProjectByName : IGetProjectByName, IScopedInjectable
{
    public string Name { get; set; }
    public Guid? Id { get; set; }
    private readonly WriteDbContext _dbContext;
    public GetProjectByName(WriteDbContext dbContext)
    => _dbContext = dbContext;

    public async Task<Project?> Query(CancellationToken cancellationToken)
    => await _dbContext.Projects.FirstOrDefaultAsync(x => x.Name == Name && (Id == null || x.Id == Id), cancellationToken);
}
