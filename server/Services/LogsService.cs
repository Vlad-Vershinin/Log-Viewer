using Newtonsoft.Json.Linq;
using server.Core.Entities;
using server.Core.Interfaces.Repositories;
using server.Core.Interfaces.Services;
using System.Collections.Generic;

namespace server.Services;

public class LogsService : ILogsService
{
    private readonly ILogsRepository _logsRepository;

    public LogsService(ILogsRepository logsRepository)
    {
        _logsRepository = logsRepository;
    }

    public void Parse(string sessionName, string json_data, string filename)
    {
        List<ParsedLog> result = new List<ParsedLog>();
        DateTime last_time = DateTime.MinValue;
        int plan_entries = 0;
        int apply_entries = 0;

        foreach (string line in json_data.Split("\n"))
        {
            ParsedLog log = new ParsedLog(filename, line, sessionName);
            try
            {
                if (line.Length > 1)
                {
                    JObject dict = JObject.Parse(line);

                    if (dict.ContainsKey("@timestamp"))
                    {
                        log.Timestamp = Convert.ToDateTime(dict["@timestamp"]);
                        last_time = log.Timestamp;
                        dict.Remove("@timestamp");
                    }
                    else { log.IsAnomaly = true; log.Timestamp = last_time.AddMicroseconds(1); }

                    if (dict.ContainsKey("@level"))
                    {
                        log.Level = dict["@level"].ToString().ToLower();
                        dict.Remove("@level");
                    }
                    else { log.IsAnomaly = true; log.Level = "missed"; }

                    if (dict.ContainsKey("@message"))
                    {
                        log.Message = dict["@message"].ToString();
                        if (log.Message.ToLower().Contains("plan")) plan_entries++;
                        if (log.Message.ToLower().Contains("apply")) apply_entries++;
                        dict.Remove("@message");
                    }
                    else { log.IsAnomaly = true; log.Message = "@message missed"; }

                    log.OtherKeys = dict;
                }
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                log.IsAnomaly = true;
            }

            result.Add(log);
        }
        _logsRepository.UploadLogsToDBAsync(result, apply_entries > plan_entries);
    }

    /*
     На этапе этой функции происходит группировка логов и парсинг вложенных тегов tf_http_req_body и tf_http_res_body
     */
    public async Task<List<ParsedLog>> GetLogs(PromptPacket prompts)
    {
        // в ключах храним уникальные req_id, в значении храним индекс первую запись с этим req_id
        Dictionary<string, int> tf_req_ids = new Dictionary<string, int>();
        List<ParsedLog> logs = await _logsRepository.GetLogsAsync(prompts);
        int index = 0;

        foreach (ParsedLog log in logs)
        {
            // Парсинг tf_http_req_body и tf_http_res_body
            if (log.OtherKeys.ContainsKey("tf_http_req_body"))
                log.OtherKeys["tf_http_req_body"] = JObject.Parse(log.OtherKeys["tf_http_req_body"].ToString());
            if (log.OtherKeys.ContainsKey("tf_http_res_body"))
                log.OtherKeys["tf_http_res_body"] = JObject.Parse(log.OtherKeys["tf_http_res_body"].ToString());

            // Вложение сгруппированных запросов
            if (log.OtherKeys.ContainsKey("tf_req_id"))
            {
                if (tf_req_ids.ContainsKey(log.OtherKeys["tf_req_id"].ToString()))
                    logs[tf_req_ids[log.OtherKeys["tf_req_id"].ToString()]].GroupedLogs.Add(log);
                else
                    tf_req_ids[log.OtherKeys["tf_req_id"].ToString()] = index;
            }
            index++;
        }

        return logs;
    }
}
