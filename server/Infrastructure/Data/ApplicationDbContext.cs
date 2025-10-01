using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Infrastructure.Data.Configurations;

namespace server.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Session> UserSessions { get; set; }
    public DbSet<ParsedLog> ParsedLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        modelBuilder.ApplyConfiguration(new ParsedLogConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
