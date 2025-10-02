using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.Entities;

namespace server.Infrastructure.Data.Configurations;

public class ParsedLogConfiguration : IEntityTypeConfiguration<ParsedLog>
{
    public void Configure(EntityTypeBuilder<ParsedLog> builder)
    {
        builder.HasKey(pl => new { pl.Filename, pl.Timestamp, pl.SessionName });

        builder
            .HasOne(us => us.Session)
            .WithMany(ps => ps.ParsedLogs)
            .HasForeignKey(pl => pl.SessionName)
            .HasPrincipalKey(s => s.SessionName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pl => pl.OtherKeysJSON)
            .HasConversion(
                v => v.ToString(),
                v => string.IsNullOrEmpty(v) ? "{}" : v
            );
    }
}
