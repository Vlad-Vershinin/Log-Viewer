using Microsoft.AspNetCore.Mvc;
using server.Core.Entities;
using server.Core.Interfaces.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("connect")]
    public async Task<IActionResult> CreateSession([FromBody] string session)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.CreateSession(new Session { SessionName = session });

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteSession([FromBody] string session)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.DeleteSession(new Session { SessionName = session });
        return Ok();
    }
}
