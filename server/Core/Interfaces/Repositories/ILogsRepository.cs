using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ILogsRepository
{
    Task<List<ParsedLog>> GetLogs(PromptPacket prompts);
    Task UploadLogsToDB(List<ParsedLog> parsedLogs);
}
