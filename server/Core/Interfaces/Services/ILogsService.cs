using server.Core.Entities;

namespace server.Core.Interfaces.Services;

public interface ILogsService
{
    void Parse(string sessionName, string json_data, string filename);
    Task<List<ParsedLog>> GetLogs(PromptPacket prompts);
    Task<List<string>> GetLogsFileNameAsync(Session session);
}
