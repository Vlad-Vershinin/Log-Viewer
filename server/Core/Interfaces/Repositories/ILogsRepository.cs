using server.Core.Entities;

namespace server.Core.Interfaces.Repositories;

public interface ILogsRepository
{
    Task<List<ParsedLog>> GetLogsAsync(PromptPacket prompts);
    Task UploadLogsToDBAsync(List<ParsedLog> parsedLogs);
    Task<List<string>> GetLogsFileName(Session session); 
}
