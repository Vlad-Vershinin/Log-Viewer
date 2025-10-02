using client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services.interfaces
{
    public interface ISessionService
    {
        void InitializeSession(string sessionName);
        public void AddLogs(IEnumerable<ParsedLog> logEntries);
        public void ClearLogs();
        public void EndSession();
        public void AddLog(ParsedLog logEntry);
    }
}
