using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ILogsRepository
{
    Task<List<ParsedLog>> GetLogsAsync(PromptPacket prompts);
    Task UploadLogsToDBAsync(List<ParsedLog> parsedLogs);
}
