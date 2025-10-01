using System.ComponentModel.DataAnnotations;

namespace server.Core.Entities;

public class Session
{
    [Required]
    public string SessionName { get; set; } = string.Empty;

    public List<ParsedLog>? ParsedLogs { get; set; }
}
