using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.Entities;

namespace server.Infrastructure.Data.Configurations;

public class ParsedLogConfiguration : IEntityTypeConfiguration<ParsedLog>
{
    public void Configure(EntityTypeBuilder<ParsedLog> builder)
    {
        builder
            .HasOne(us => us.UserSession)
            .WithMany(ps => ps.ParsedLogs)
            .HasForeignKey(pl => pl.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
