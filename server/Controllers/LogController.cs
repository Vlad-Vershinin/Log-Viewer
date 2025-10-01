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

    [HttpPost("load")]
    public async Task<IActionResult> UploadLogs(FilesPacket dto)
    {
        //using var reader = new StreamReader(Request.Body);
        //var body = await reader.ReadToEndAsync();
        
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

    //[HttpPost("get")]
    //public async Task<List<ParsedLog>> GetLogs(PromptPacket dto)
    //{

    //}
}
