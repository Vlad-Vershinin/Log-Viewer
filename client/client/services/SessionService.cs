using client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    public class SessionService : ReactiveObject
    {
        private static readonly Lazy<SessionService> _instance =
            new Lazy<SessionService>(() => new SessionService());

        public static SessionService Instance => _instance.Value;

        private string _sessionName;
        public string SessionName
        {
            get => _sessionName;
            set => this.RaiseAndSetIfChanged(ref _sessionName, value);
        }

        private readonly ObservableCollection<LogEntry> _logs;
        public ReadOnlyObservableCollection<LogEntry> Logs { get; }

        private SessionService()
        {
            _logs = new ObservableCollection<LogEntry>();
            Logs = new ReadOnlyObservableCollection<LogEntry>(_logs);
        }

        public void InitializeSession(string sessionName)
        {
            SessionName = sessionName;
            _logs.Clear();
        }

        public void AddLog(LogEntry logEntry)
        {
            _logs.Add(logEntry);
        }

        public void AddLogs(IEnumerable<LogEntry> logEntries)
        {
            foreach (var log in logEntries)
            {
                _logs.Add(log);
            }
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }

        public void EndSession()
        {
            SessionName = string.Empty;
            _logs.Clear();
        }
    }
}
