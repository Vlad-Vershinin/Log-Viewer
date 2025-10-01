using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace server.Core.Entities;

public class ParsedLog
{
    public ParsedLog(string filename, string rawJSON)
    {
        Filename = filename;
        RawJSON = rawJSON;
    }

    [Required]
    public string Filename { get; }
    public string RawJSON { get; }
    public DateTime Timestamp { get; set; }
    [Required]
    public string Level { get; set; } = string.Empty; // ['info', 'debug', 'trace', 'warn', 'error']
    public string Message { get; set; } = string.Empty;
    public List<JToken>? OtherKeys { get; set; }
    public bool IsHidden { get; set; } = false;
    public bool IsAnomaly { get; set; } = false;
    public string SessionName { get; set; } = string.Empty;

    // for db navigation
    public Session? Session { get; set; }
}