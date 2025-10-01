using Newtonsoft.Json.Linq;

namespace server.Services.ParserService;

public class ParsedLog
{
    public ParsedLog(string filename, string rawJSON)
    {
        Filename = filename;
        RawJSON = rawJSON;
    }

    public string Filename { get; }
    public string RawJSON { get; }
    public DateTime Timestamp { get; set; }
    public string Level { get; set; } // ['info', 'debug', 'trace', 'warn', 'error']
    public string Message { get; set; }
    public List<JToken> OtherKeys { get; set; }
    public bool IsHidden { get; set; } = false;
    public bool IsAnomaly { get; set; } = false;
}