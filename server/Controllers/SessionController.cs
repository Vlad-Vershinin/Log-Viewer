using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateSession([FromForm] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Name is empty");
        }

        return Ok();
    }
}
