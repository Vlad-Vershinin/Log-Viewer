using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/[controler]")]
public class LogController : ControllerBase
{
    public LogController()
    {

    }

    [HttpPost("log")]
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
