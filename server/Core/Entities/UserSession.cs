using System.ComponentModel.DataAnnotations;

namespace server.Core.DTOs.Entities;

public class UserSession
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string UserSessionName { get; set; } = string.Empty;

    public List<ParsedLog>? ParsedLogs { get; set; }
}
