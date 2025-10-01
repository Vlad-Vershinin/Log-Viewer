using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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
        public ReactiveCommand<Unit, Unit> OpenOptionPane { get; set; }


        [Reactive]
        public bool IsOptionPaneIsOpen { get; set; } = false;


        private ObservableCollection<ParsedLog> _parsedLogs;
        public HierarchicalTreeDataGridSource<ParsedLog> LogsSource { get; }


<<<<<<< HEAD
        private readonly ObservableCollection<LogEntry> _logEntries;

        private readonly INavigationService _navigationService;

        public LogTableViewModel(INavigationService navigationService)
        {
            _logEntries = new ObservableCollection<LogEntry>();

            InitializeSampleData();

            _navigationService = navigationService;
=======
        public LogTableViewModel()
        {
            CreateBoardCommand = ReactiveCommand.Create(SwitchToDiagramPage);
            OpenOptionPane = ReactiveCommand.CreateFromTask(OpenPane);

            LogsSource = new HierarchicalTreeDataGridSource<ParsedLog>(_parsedLogs)
            {
                Columns =
                {
                    new CheckBoxColumn<ParsedLog>("Скрыть", x=>x.IsHidden),
                    new HierarchicalExpanderColumn<ParsedLog>(
                        new TextColumn<ParsedLog, string>("Сообщение", x => x.Message), x=>x.GroupedLogs),

                },
            };
                
        }

        /*
 <DataGridCheckBoxColumn Header="Скрыть" Binding="{Binding IsHidden}" Width="0.15*"/>
					<DataGridTextColumn Header="Время" Binding="{Binding Timestamp}" Width="0.4*"/>
					<DataGridTextColumn Header="Вид лога" Binding="{Binding Level}" Width="0.2*"/>
					<DataGridTemplateColumn Header="Содержание" Binding="{Binding Message}" Width="0.5*"/>
 */
        public ObservableCollection<LogEntry> LogEntries => _logEntries;

        private async Task OpenPane()
        {
            IsOptionPaneIsOpen = true;
        }
        private void SwitchToDiagramPage()
        {
            // Blank
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
