using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ISessionRepositry
{
    Task CreateSessionAsync(UserSession session);
    Task GetSessionAsync(UserSession session);
    Task<List<ParsedLog>> LoadSessionAsync(UserSession session);
    Task DeleteSessionAsync(UserSession session);
}
