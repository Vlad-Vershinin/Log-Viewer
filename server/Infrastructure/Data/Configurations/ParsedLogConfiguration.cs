using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.Entities;

namespace server.Infrastructure.Data.Configurations;

public class ParsedLogConfiguration : IEntityTypeConfiguration<ParsedLog>
{
    public void Configure(EntityTypeBuilder<ParsedLog> builder)
    {
        builder.HasKey(pl => new { pl.Filename, pl.Timestamp });

        builder
            .HasOne(us => us.Session)
            .WithMany(ps => ps.ParsedLogs)
            .HasForeignKey(pl => pl.SessionName)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
