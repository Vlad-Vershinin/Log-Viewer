using Microsoft.EntityFrameworkCore;
using server.Infrastructure.Data.Configurations;

namespace server.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        modelBuilder.ApplyConfiguration(new ParsedLogConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
