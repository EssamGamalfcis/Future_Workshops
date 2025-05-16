using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Contexts;
public partial class WriteDbContext : BaseDbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }
}

