using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ISessionRepositry
{
    Task CreateSessionAsync(Session session);
    Task GetSessionAsync(Session session);
    Task<List<ParsedLog>> LoadSessionAsync(Session session);
    Task DeleteSessionAsync(Session session);
}
