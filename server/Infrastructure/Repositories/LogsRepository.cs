using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Infrastructure.Data;

namespace server.Infrastructure.Repositories;

public class LogsRepository : ILogsRepository
{
    private readonly ApplicationDbContext _context;

    public LogsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<ParsedLog>> GetLogs(PromptPacket prompts)
    {
        throw new NotImplementedException();
    }

    public async Task UploadLogsToDB(List<ParsedLog> parsedLogs)
    {
        foreach (ParsedLog log in parsedLogs) {
            await _context.ParsedLogs.AddAsync(log);
        }
    }
}