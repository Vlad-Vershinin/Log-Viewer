using client.services;
using client.services.interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;


namespace client.ViewModels
{
    

    public class LogTableViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CreateBoardCommand { get; set; }

        private readonly ObservableCollection<LogEntry> _logEntries;

        private readonly INavigationService _navigationService;

        public LogTableViewModel(INavigationService navigationService)
        {
            _logEntries = new ObservableCollection<LogEntry>();

            InitializeSampleData();

            _navigationService = navigationService;
        }

        public ObservableCollection<LogEntry> LogEntries => _logEntries;

        public ObservableCollection<LogEntry> LogEntries1 => _logEntries;

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

            foreach (var log in logs)
            {
                _logEntries.Add(log);
            }
        }

    }

}
