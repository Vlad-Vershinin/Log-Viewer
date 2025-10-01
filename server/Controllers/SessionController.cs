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
    public async Task<IActionResult> CreateSession([FromForm] string session)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.CreateSession(new UserSession { UserSessionName = session });

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteSession([FromForm] string session)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            return BadRequest("Session name is empty");
        }

        await _sessionService.DeleteSession(new UserSession { UserSessionName = session });
        return Ok();
    }
}
