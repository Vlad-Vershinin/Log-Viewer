using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Core.Entities;

public class ParsedLog
{
    public ParsedLog() { }

    public ParsedLog(string filename, string rawJSON)
    {
        Filename = filename;
        RawJSON = rawJSON;
    }

    [Required]
    public string Filename { get; set; } = string.Empty;
    public string RawJSON { get; set; } = string.Empty;
    [Required]
    public DateTime Timestamp { get; set; }
    [Required]
    public string Level { get; set; } = string.Empty; // ['info', 'debug', 'trace', 'warn', 'error', '@level missed']
    public string Message { get; set; } = string.Empty;
    public string OtherKeysJSON
    { 
        get => other_keys?.ToString() ?? "{}";
        set => this.other_keys = JObject.Parse(value);
    } // это в бд суём, это строка в JSON нотации

    [Newtonsoft.Json.JsonIgnore]
    [NotMapped]
    public JObject OtherKeys 
    { 
        get => other_keys;
        set => this.other_keys = value;
    } // это в бд не суём
    public bool IsHidden { get; set; } = false;
    public bool IsAnomaly { get; set; } = false;
    public List<ParsedLog>? GroupedLogs { get; set; } // логи из этого листа распоковываем как отдельные записи в бд

    public string SessionName { get; set; } = string.Empty; // хранит имя сессии Session
    public Session? Session { get; set; } // for db navigation

    private JObject? other_keys;
}