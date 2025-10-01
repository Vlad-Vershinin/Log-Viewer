using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Layout;
using Avalonia.Media;
using client.ViewModels.Charts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat.ModeDetection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;




using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace client.ViewModels
{
    

    public class LogTableViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CreateBoardCommand { get; set; }
        public ReactiveCommand<Unit, Unit> OpenOptionPane { get; set; }




        public ReactiveCommand<Unit, Unit> NextPage { get; set; }
        public ReactiveCommand<Unit, Unit> PreviousPage { get; set; }
        public ReactiveCommand<Unit, Unit> FirstPage { get; set; }
        public ReactiveCommand<Unit, Unit> LastPage { get; set; }


        [Reactive]
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; } = 100;






        [Reactive]
        public bool IsOptionPaneIsOpen { get; set; } = false;


        private ObservableCollection<ParsedLog> _parsedLogs;
        public HierarchicalTreeDataGridSource<ParsedLog> LogsSource { get; }

        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();

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


            NextPage = ReactiveCommand.CreateFromTask(ToNextPage);
            PreviousPage = ReactiveCommand.CreateFromTask(ToPreviousPage);
            LastPage = ReactiveCommand.CreateFromTask(ToLastPage);
            FirstPage = ReactiveCommand.CreateFromTask(ToFirstPage);






            _parsedLogs = new ObservableCollection<ParsedLog>();

            ParsedLog parsedLog = new ParsedLog("wdawd", "wadadwddddd");
            ParsedLog parsedLog1 = new ParsedLog("wdawd", "wadadwddddd");

            parsedLog.Message = "dddddddddd";
            parsedLog.GroupedLogs = new List<ParsedLog>();
            parsedLog1.Message = "aaaaa";
            parsedLog1.IsHidden = true;
            parsedLog.GroupedLogs.Add(parsedLog1);

            _parsedLogs.Add(parsedLog);


            LogsSource = new HierarchicalTreeDataGridSource<ParsedLog>(_parsedLogs)
            {
                Columns =
                {
                    new CheckBoxColumn<ParsedLog>("Скрыть", x=>x.IsHidden, (x, value) =>{x.IsHidden = value;  }),
                    new HierarchicalExpanderColumn<ParsedLog>(new TextColumn<ParsedLog, string>("Время", x => x.TimestampStr), x=>x.GroupedLogs),
                    new TextColumn<ParsedLog, string>("Сообщение", x => x.Message)
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
