using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    [HttpPost("load")]
    public async Task<IActionResult> UploadLogs()
    {
        if (string.IsNullOrEmpty(Request.Body.ToString()))
            return BadRequest("File is empty");
        else
        {
            return Ok();
        }
    }
}
