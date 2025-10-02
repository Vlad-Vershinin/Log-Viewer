using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using client.services;
using client.services.interfaces;
using client.ViewModels.Charts;
using client.Views;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat.ModeDetection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;
using System.Reactive.Joins;
using System.Text;
using System.Threading.Tasks;




using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using client.Models;
using client.services;

namespace client.ViewModels
{


    public class LogTableViewModel : ViewModelBase
    {
        // commands for DataGrid
        [Reactive]
        public string SessionName { get; set; } = string.Empty;

        [Reactive]
        public string Filename { get; set; } = string.Empty;

        [Reactive]
        public int Pivot { get; set; } = 0;

        [Reactive]
        public int Page { get; set; } = 1;

        [Reactive]
        public int LogsPerPage { get; set; } = 50;

        [Reactive]
        public bool ShowHidden { get; set; } = false;

        [Reactive]
        public bool PartialComparing { get; set; } = false;

        [Reactive]
        public string SearchPrompt { get; set; } = string.Empty;

        [Reactive]
        public string LevelFilter { get; set; } = string.Empty;


        // other commands
        public ReactiveCommand<Unit, Unit> CreateBoardCommand { get; set; }
        public ReactiveCommand<Unit, Unit> OpenOptionPane { get; set; }
        



        public ReactiveCommand<Unit, Unit> NextPage { get; set; }
        public ReactiveCommand<Unit, Unit> PreviousPage { get; set; }
        public ReactiveCommand<Unit, Unit> FirstPage { get; set; }
        public ReactiveCommand<Unit, Unit> LastPage { get; set; }


        public ReactiveCommand<Unit, Unit> LoadLogsCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteSessionCommand { get; set; }


        public ReactiveCommand<Unit, Unit> ApplyClampTrigger { get; set; }


        [Reactive]
        public int CurrentPage { get; set; } = 1;
        public string CurrentPageStr { get { return $"{CurrentPage}"; } }
        public int MaxPage { get; set; } = 100;



       








        [Reactive]
        public bool IsOptionPaneIsOpen { get; set; } = false;

        [Reactive]
        public bool IsFullInfoPaneIsOpen { get; set; } = true;





        [Reactive]
        public ParsedLog SelectedLog { get; set; }


        [Reactive]
        public ParsedLog DoubleTappedLog { get; set; }

        public ReactiveCommand<ParsedLog, Unit> DoubleClickCommand { get; }





        private ObservableCollection<ParsedLog> _parsedLogs;
        private ObservableCollection<LogEntry> _logsFromLogEntry;
        public HierarchicalTreeDataGridSource<ParsedLog> LogsSource { get; }
        public HierarchicalTreeDataGridSource<LogEntry> EntryLogsSource { get; }








        // Charts
        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();
        public GanttDiagramViewModel GanttChart { get; set; } = new GanttDiagramViewModel();




        [Reactive]
        public List<AssignedFileTest> FilesAssigned { get; set; }






        private readonly SessionService _sessionService;


        public LogTableViewModel(SessionService sessionService)
        {
            _httpClient = clientService;
            _sessionService = sessionService;
            _navigationService = navigationService;


            




            CreateBoardCommand = ReactiveCommand.Create(SwitchToDiagramPage);
            OpenOptionPane = ReactiveCommand.CreateFromTask(OpenPane);


            NextPage = ReactiveCommand.CreateFromTask(ToNextPage);
            PreviousPage = ReactiveCommand.CreateFromTask(ToPreviousPage);
            LastPage = ReactiveCommand.CreateFromTask(ToLastPage);
            FirstPage = ReactiveCommand.CreateFromTask(ToFirstPage);

            LoadLogsCommand = ReactiveCommand.CreateFromTask(LoadLogs);
            DeleteSessionCommand = ReactiveCommand.CreateFromTask(DeleteSession);

            ApplyClampTrigger = ReactiveCommand.CreateFromTask(ApplyClamp);


            DoubleClickCommand = ReactiveCommand.CreateFromTask<ParsedLog>(log =>
            OpenFullInfoPane(log)
            );



            _parsedLogs = new ObservableCollection<ParsedLog>();
            _logsFromLogEntry = new ObservableCollection<LogEntry>();
            _logsFromLogEntry = _sessionService.Logs;



            /*
            EntryLogsSource = new HierarchicalTreeDataGridSource<LogEntry>(_logsFromLogEntry)
            {
                Columns =
                {
                    new CheckBoxColumn<LogEntry>("Скрыть", x=>x.IsHidden, (x, value) =>{x.IsHidden = value;  }),
                    new HierarchicalExpanderColumn<LogEntry>(new TextColumn<LogEntry, string>("Время", x => x.TimeStr), x=>x.GroupedLogs),
                    new TextColumn<LogEntry, string>("Сообщение", x => x.Content)
                },
            };
            */

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


            
        




        public async Task OpenFullInfoPane(ParsedLog log)
        {
            DoubleTappedLog = log;
            IsFullInfoPaneIsOpen = true;
        }



        public async Task ApplyClamp()
        {
            CurrentPage = Math.Clamp(CurrentPage, 1, MaxPage);
        }

        public async Task ToNextPage()
        {
            CurrentPage++;
            ApplyClamp();
        }
        public async Task ToPreviousPage()
        {
            CurrentPage--;
            ApplyClamp();
        }
        public async Task ToLastPage()
        {
            CurrentPage++;
            ApplyClamp();

        }
        public async Task ToFirstPage()
        {
            CurrentPage = 1;
            ApplyClamp();
        }







        private async Task OpenPane()
        {
            IsOptionPaneIsOpen = true;
        }
        private void SwitchToDiagramPage()
        {
            // Blank
        }


        private async Task LoadLogs()
        {
            var window = App.ServiceProvider.GetService<MainWindow>();

            var fileTypes = new FilePickerFileType[]
            {
                new FilePickerFileType("JSON Files")
                {
                    Patterns = new[] { "*.json" },
                    AppleUniformTypeIdentifiers = new[] { "public.json" },
                    MimeTypes = new[] { "application/json" }
                },
                FilePickerFileTypes.TextPlain
            };

            var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Выберите файлы логов",
                AllowMultiple = true,
                FileTypeFilter = fileTypes
            });

            if (files == null || !files.Any())
                return;


            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(_sessionService.SessionName), "SessionName");

            foreach (var file in files)
            {
                var fileContent = new StreamContent(await file.OpenReadAsync());
                content.Add(fileContent, "Files", file.Name);
            }

            var response = await _httpClient.HttpClient.PostAsync("log/logs", content);

            var fileNamesList = await response.Content.ReadFromJsonAsync<List<string>>();
            _sessionService.FileNames = new ObservableCollection<string>(fileNamesList ?? new List<string>());
        }

        private async Task DeleteSession()
        {
            var res = await _httpClient.HttpClient.DeleteAsync($"session/delete/{_sessionService.SessionName}");

            if (res.IsSuccessStatusCode)
            {
                _sessionService.CloseSession();
                _navigationService.NavigateTo<LoginView>();
            }
        }
    }

}