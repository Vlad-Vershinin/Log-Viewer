using Microsoft.EntityFrameworkCore;
using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Infrastructure.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
        var req = _context.ParsedLogs
            .Where(log => log.SessionName == prompts.SessionName)
            .Where(log => log.Filename == prompts.Filename)
            .Where(log => prompts.LevelFilter.Contains(log.Level))
            .Where(log => prompts.SearchPrompt.Length == 0 ? true : (prompts.PartialComparing ? StringPartialComparer.Compare(log.Timestamp.ToString(), prompts.SearchPrompt) : log.Timestamp.ToString().Contains(prompts.SearchPrompt)
            || prompts.PartialComparing ? StringPartialComparer.Compare(log.Message.ToString(), prompts.SearchPrompt) : log.Message.ToString().Contains(prompts.SearchPrompt)
            || prompts.PartialComparing ? StringPartialComparer.Compare(log.Level.ToString(), prompts.SearchPrompt) : log.Level.ToString().Contains(prompts.SearchPrompt)
            || prompts.PartialComparing ? StringPartialComparer.JObjectCompare(log.OtherKeys, prompts.SearchPrompt) : log.Timestamp.ToString().Contains(prompts.SearchPrompt)));

        if (!prompts.ShowHidden)
        {
            req.Where(log => !log.IsHidden);
        }
        
        List<ParsedLog> res = await req.Skip(prompts.LogsPerPage * prompts.Page + prompts.Pivot).Take(prompts.LogsPerPage).ToListAsync();

        return res ?? new List<ParsedLog>();
    }

    public async Task<List<string>> GetLogsFileName(Session session)
    {
        return await _context.ParsedLogs
            .Where(pl => pl.SessionName == session.SessionName)
            .Select(pl => pl.Filename)
            .Distinct()
            .ToListAsync();
    }

    public async Task UploadLogsToDBAsync(List<ParsedLog> parsedLogs)
    {
        foreach (ParsedLog log in parsedLogs) { log.Filename = (isApply ? "[apply] " : "[plan] ") + log.Filename; }

        foreach (ParsedLog log in parsedLogs) {
            await _context.ParsedLogs.AddAsync(log);
        }
        await _context.SaveChangesAsync();
    }
}