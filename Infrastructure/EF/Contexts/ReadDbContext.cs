using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts;

public partial class ReadDbContext : BaseDbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseNpgsql();
    }
}
