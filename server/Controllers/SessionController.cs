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

    [HttpPost("connect/{SessionName}")]
    public async Task<IActionResult> CreateSession(string SessionName)
    {
        if (string.IsNullOrWhiteSpace(SessionName))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.CreateSession(new Session { SessionName = SessionName });

        return Ok();
    }

    [HttpDelete("delete/{sessionName}")]
    public async Task<IActionResult> DeleteSession(string sessionName)
    {
        if (string.IsNullOrWhiteSpace(sessionName))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.DeleteSession(new Session { SessionName = sessionName });
        return Ok();
    }
}
