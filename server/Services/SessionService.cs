using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Core.Interfaces.Services;

namespace server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepositry _sessionRepositry;

    public SessionService(ISessionRepositry sessionRepositry)
    {
        _sessionRepositry = sessionRepositry;
    }

    public async Task CreateSession(UserSession userSession)
    {
        if (string.IsNullOrWhiteSpace(userSession.UserSessionName))
        {
            return;
        }

        await _sessionRepositry.CreateSessionAsync(userSession);
    }

    public async Task DeleteSession(UserSession userSession)
    {
        await _sessionRepositry.DeleteSessionAsync(userSession);
    }
}
