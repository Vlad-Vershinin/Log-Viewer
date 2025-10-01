using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Infrastructure.Data;

namespace server.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateSessionAsync(Session session)
    {
        await _context.UserSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSessionAsync(Session session)
    {
        var entity = await _context.UserSessions.FindAsync(session.SessionName);
        _context.UserSessions.Remove(entity!);
        await _context.SaveChangesAsync();
    }

    public async Task GetSessionAsync(Session session)
    {
        return;
    }
}
