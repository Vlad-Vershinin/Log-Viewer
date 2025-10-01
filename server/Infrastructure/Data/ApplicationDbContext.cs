using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Infrastructure.Data.Configurations;

namespace server.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    DbSet<Session> UserSessions { get; set; }
    DbSet<ParsedLog> ParsedLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        modelBuilder.ApplyConfiguration(new ParsedLogConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
