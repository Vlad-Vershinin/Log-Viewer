using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace server.Core.Entities;

public class ParsedLog
{
    public ParsedLog() { }

    public ParsedLog(string filename, string rawJSON, string sessionName)
    {
        Filename = filename;
        RawJSON = rawJSON;
        SessionName = sessionName;
    }

    [Required]
    public string Filename { get; set; }
    public string RawJSON { get; set; }
    [Required]
    public DateTime Timestamp { get; set; }
    [Required]
    public string Level { get; set; } = string.Empty; // ['info', 'debug', 'trace', 'warn', 'error', '@level missed']
    public string Message { get; set; } = string.Empty;
    public string OtherKeysJSON { get { return other_keys.ToString(); } set { this.other_keys = JObject.Parse(value); } } // это в бд суём, это строка в JSON нотации
    public JObject OtherKeys { get { return other_keys; } set { this.other_keys = value; } } // это в бд не суём
    public bool IsHidden { get; set; } = false;
    public bool IsAnomaly { get; set; } = false;
    public List<ParsedLog> GroupedLogs { get; set; } // логи из этого листа распаковываем как отдельные записи в бд

    public string SessionName { get; set; } = string.Empty; // хранит имя сессии Session
    public Session? Session { get; set; } // for db navigation

    private JObject other_keys;
}