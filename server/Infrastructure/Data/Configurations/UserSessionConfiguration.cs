using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.Entities;

namespace server.Infrastructure.Data.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(us => us.SessionName);

        builder
            .HasMany(pl => pl.ParsedLogs)
            .WithOne(us => us.Session);
    }
}
