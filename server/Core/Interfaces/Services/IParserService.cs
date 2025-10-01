using server.Core.Entities;

namespace server.Core.Interfaces.Services;

public interface IParserService
{
    List<ParsedLog> Parse(string json_data, string filename);
}
