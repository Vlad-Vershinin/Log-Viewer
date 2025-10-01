using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ParsedLog
{
    public ParsedLog(string filename, string rawJSON)
    {
        Filename = filename;
        RawJSON = rawJSON;
    }

    [Required]
    public string Filename { get; } = string.Empty;
    public string RawJSON { get; } = string.Empty;
    [Required]
    public DateTime Timestamp { get; set; }
    [Required]
    public string Level { get; set; } = string.Empty; // ['info', 'debug', 'trace', 'warn', 'error', '@level missed']
    public string Message { get; set; } = string.Empty;
    public JObject OtherKeys { get { return other_keys; } set { this.other_keys = value; } } // это в бд не суём
    public bool IsHidden { get; set; } = false;
    public bool IsAnomaly { get; set; } = false;
    public List<ParsedLog>? GroupedLogs { get; set; } // логи из этого листа распоковываем как отдельные записи в бд

    private JObject other_keys;
}