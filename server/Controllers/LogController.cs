using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using server.Core.Interfaces.Services;
using server.Core.Entities;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly ILogsService _parserService;

    public LogController(ILogsService parserService)
    {
        _parserService = parserService;
    }

    [HttpPost("logs")]
    public async Task<IActionResult> UploadLogs([FromForm] FilesPacket dto)
    {
        //using var reader = new StreamReader(Request.Body);
        //var body = await reader.ReadToEndAsync();

        if (dto == null)
        {
            return BadRequest("DTO is null");
        }

        if (dto.Files == null || !dto.Files.Any())
        {
            return BadRequest("No files provided");
        }

        if (string.IsNullOrEmpty(dto.SessionName))
        {
            return BadRequest("Session name is required");
        }

        foreach (IFormFile file in dto.Files)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            _parserService.Parse(dto.SessionName, result.ToString(), file.FileName);
        }

        return Ok();
    }

    [HttpGet("logs")]
    public async Task<List<ParsedLog>> GetLogs(PromptPacket dto)
    {
        return await _parserService.GetLogs(dto);
    }
}
