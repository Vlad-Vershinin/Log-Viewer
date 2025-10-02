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

    public async Task<List<ParsedLog>> GetLogsAsync(PromptPacket prompts)
    {
        var res = await _context.ParsedLogs
            .Where(log => log.SessionName == prompts.SessionName && log.Filename == prompts.Filename && (!log.IsHidden || prompts.ShowHidden))
            .Skip(prompts.LogsPerPage * prompts.Page + prompts.Pivot)
            .Take(prompts.LogsPerPage)
            .ToListAsync();

        return res ?? new List<ParsedLog>();
    }

    public async Task UploadLogsToDBAsync(List<ParsedLog> parsedLogs)
    {
        foreach (ParsedLog log in parsedLogs) {
            await _context.ParsedLogs.AddAsync(log);
        }
        await _context.SaveChangesAsync();
    }
}