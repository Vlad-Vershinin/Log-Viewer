using server.Core.Entities;

namespace server.Core.Interfaces.Services;

public interface ISessionService
{
    Task CreateSession(Session userSession);
    Task DeleteSession(Session userSession);
}
