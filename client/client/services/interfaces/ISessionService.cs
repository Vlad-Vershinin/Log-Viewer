using System.Collections.Generic;
using client.Models;

namespace client.services.interfaces
{
    public interface ISessionService
    {
        void InitializeSession(string sessionName);
        public void AddLogs(IEnumerable<TextColour> logEntries);
        public void ClearLogs();
        public void EndSession();
        public void AddLog(TextColour logEntry);
    }
}
