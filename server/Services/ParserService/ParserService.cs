using Newtonsoft.Json.Linq;
using server.Core.Entities;
using server.Core.Interfaces;

namespace server.Services.ParserService;

public class ParserService : IParserService
{
    public List<ParsedLog> Parse(string json_data, string filename)
    {
        List<ParsedLog> result = new List<ParsedLog>();
        foreach (string line in json_data.Split("\n"))
        {
            ParsedLog log = new ParsedLog(filename, line);
            try
            {
                JObject dict = JObject.Parse(line);
                
                if (dict.ContainsKey("@timestamp"))
                {
                    log.Timestamp = Convert.ToDateTime(dict["@timestamp"]);
                    dict["@timestamp"].Remove();
                } else log.IsAnomaly = true;

                if (dict.ContainsKey("@level"))
                {
                    log.Level = dict["@level"].ToString();
                    dict["@level"].Remove();
                } else log.IsAnomaly = true;

                if (dict.ContainsKey("@message"))
                {
                    log.Message = dict["@message"].ToString();
                    dict["@message"].Remove();
                } else log.IsAnomaly = true;

                log.OtherKeys = dict.Children().ToList();
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                log.IsAnomaly = true;
            }

            result.Add(log);
        }
        return result;
    }
}
