using Newtonsoft.Json.Linq;
using server.Core.Entities;
using server.Core.Interfaces.Services;
using server.Core.Interfaces.Repositories;

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
        Dictionary<string, int> tf_req_ids = new Dictionary<string, int>(); // в ключах храним уникальные req_id, в значении храним индекс первую запись с этим req_id
        DateTime last_time = DateTime.MinValue;
        int index = 0;

        foreach (string line in json_data.Split("\n"))
        {
            ParsedLog log = new ParsedLog(filename, line, sessionName);
            try
            {
                JObject dict = JObject.Parse(line);
                
                if (dict.ContainsKey("@timestamp"))
                {
                    log.Timestamp = Convert.ToDateTime(dict["@timestamp"]);
                    last_time = log.Timestamp;
                    dict.Remove("@timestamp");
                } else { log.IsAnomaly = true; log.Timestamp = last_time.AddMicroseconds(1); }

                if (dict.ContainsKey("@level"))
                {
                    log.Level = dict["@level"].ToString();
                    dict.Remove("@level");
                } else { log.IsAnomaly = true; log.Level = "@level missed"; }

                if (dict.ContainsKey("@message"))
                {
                    log.Message = dict["@message"].ToString();
                    dict.Remove("@message");
                } else { log.IsAnomaly = true; log.Message = "@message missed"; }

                if (dict.ContainsKey("tf_req_id"))
                {
                    if (tf_req_ids.ContainsKey(dict["tf_req_id"].ToString()))
                        result[tf_req_ids[dict["tf_req_id"].ToString()]].GroupedLogs.Add(log);
                    else
                        tf_req_ids[dict["tf_req_id"].ToString()] = index;
                }

                log.OtherKeys = dict;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                log.IsAnomaly = true;
            }

            result.Add(log);
            index++;
        }
        _logsRepository.UploadLogsToDBAsync(result);
    }

    public async Task<List<ParsedLog>> GetLogs(PromptPacket prompts)
    {
        return await _logsRepository.GetLogsAsync(prompts);
    }
}
