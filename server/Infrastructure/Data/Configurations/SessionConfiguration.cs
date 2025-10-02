using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.Entities;

namespace server.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(us => us.SessionName);

        builder
            .HasMany(pl => pl.ParsedLogs)
            .WithOne(us => us.Session)
            .HasForeignKey(pl => pl.SessionName)
            .HasPrincipalKey(s => s.SessionName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.SessionName)
            .IsUnique();

        builder.Property(s => s.SessionName)
            .IsRequired()
            .HasMaxLength(32);
    }
}
