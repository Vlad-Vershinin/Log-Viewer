using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Infrastructure.Data;

namespace server.Infrastructure.Repositories;

public class SessionRepositry : ISessionRepositry
{
    private readonly ApplicationDbContext _context;

    public SessionRepositry(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateSessionAsync(UserSession session)
    {
        await _context.UserSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSessionAsync(UserSession session)
    {
        var entity = await _context.UserSessions.FindAsync(session.UserSessionName);
        _context.UserSessions.Remove(entity!);
        await _context.SaveChangesAsync();
    }

    public async Task GetSessionAsync(UserSession session)
    {
        return;
    }

    public async Task<List<ParsedLog>> LoadSessionAsync(UserSession session)
    {
        var res = await _context.UserSessions
            .Where(s => s.UserSessionName == session.UserSessionName)
            .Include(s => s.ParsedLogs)
            .FirstOrDefaultAsync();

        return res?.ParsedLogs ?? new List<ParsedLog>();
    }
}
