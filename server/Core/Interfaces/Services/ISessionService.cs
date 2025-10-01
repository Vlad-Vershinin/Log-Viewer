using server.Core.Entities;

namespace server.Core.Interfaces.Services;

public interface ISessionService
{
    Task CreateSession(UserSession userSession);
    Task DeleteSession(UserSession userSession);
}
