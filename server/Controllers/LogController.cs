using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    [HttpPost("load")]
    public async Task<IActionResult> UploadLogs()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        
        if (string.IsNullOrEmpty(body))
            return BadRequest("File is empty");
        else
        {
            return Ok();
        }
    }
}
