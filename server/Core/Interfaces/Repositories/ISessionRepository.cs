using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ISessionRepository
{
    Task CreateSessionAsync(Session session);
    Task GetSessionAsync(Session session);
    Task DeleteSessionAsync(Session session);
    Task<bool> IfSessionExist(Session session);
}
