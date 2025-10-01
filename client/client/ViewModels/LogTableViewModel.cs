using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;


namespace client.ViewModels
{
    

    public class LogTableViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CreateBoardCommand { get; set; }

        private readonly ReactiveList<LogEntry> _logEntries;

        public LogTableViewModel()
        {
            _logEntries = new ReactiveList<LogEntry>();

            InitializeSampleData();
        }

        public IReactiveList<LogEntry> LogEntries => _logEntries;

        public ReactiveList<LogEntry> LogEntries1 => _logEntries;

        private void InitializeSampleData()
        {
            var logs = new List<LogEntry>
        {
            new() { Time = DateTime.Now, Type = "info", Content = "Application started" },
            new() { Time = DateTime.Now, Type = "debug", Content = "Debug information" },
            new() { Time = DateTime.Now, Type = "warn", Content = "Warning message" },
            new() { Time = DateTime.Now, Type = "error", Content = "Error occurred" },
            new() { Time = DateTime.Now, Type = "trace", Content = "Trace details" }
        };

            _logEntries.AddRange(logs);
        }

    }

}
