using client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    public class PromptService
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<LogEntry>? Logs { get; set; } = [];

        public PromptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task GetLogsAsync(PromptPacket packet)
        {
            var query = $"?SessionName={packet.SessionName}" +
                        $"&Filename={packet.Filename}" +
                        $"&Pivot={packet.Pivot}" +
                        $"&Page={packet.Page}" +
                        $"&LogsPerPage={packet.LogsPerPage}" +
                        $"&ShowHidden={packet.ShowHidden}" +
                        $"&PartialComparing={packet.PartialComparing}" +
                        $"&SearchPrompt={packet.SearchPrompt}" +
                        $"&LevelFilter={packet.LevelFilter}";

            var response = await _httpClient.GetAsync($"logs/logs/{query}");
            response.EnsureSuccessStatusCode();

            var logs = await response.Content.ReadFromJsonAsync<List<LogEntry>>();
            Logs?.Clear();
            if (logs != null)
            {
                foreach (var log in logs)
                {
                    Logs?.Add(log);
                }
            }
        }
        
    }
}
