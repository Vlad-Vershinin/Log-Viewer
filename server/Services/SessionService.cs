using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Core.Interfaces.Services;

namespace server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepositry;

    public SessionService(ISessionRepository sessionRepositry)
    {
        _sessionRepositry = sessionRepositry;
    }

    public async Task CreateSession(Session userSession)
    {
        if (string.IsNullOrWhiteSpace(userSession.SessionName))
        {
            return;
        }

        await _sessionRepositry.CreateSessionAsync(userSession);
    }

    public async Task DeleteSession(Session userSession)
    {
        await _sessionRepositry.DeleteSessionAsync(userSession);
    }
}
