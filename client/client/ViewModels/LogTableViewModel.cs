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

namespace client.ViewModels
{


    public class LogTableViewModel : ViewModelBase
    {
        private readonly HttpClientService _httpClient;
        private readonly SessionService _sessionService;
        private readonly INavigationService _navigationService;

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
        public string SearchPrompt { get; set; } = string.Empty;








        [Reactive]
        public bool IsOptionPaneIsOpen { get; set; } = false;

        [Reactive]
        public bool IsFullInfoPaneIsOpen { get; set; } = false;





        [Reactive]
        public ParsedLog SelectedLog { get; set; }


        [Reactive]
        public ParsedLog DoubleTappedLog { get; set; }

        public ReactiveCommand<ParsedLog, Unit> DoubleClickCommand { get; }





        private ObservableCollection<ParsedLog> _parsedLogs;
        public HierarchicalTreeDataGridSource<ParsedLog> LogsSource { get; }




        // Charts
        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();
        public GanttDiagramViewModel GanttChart { get; set; } = new GanttDiagramViewModel();


        public LogTableViewModel(HttpClientService clientService, SessionService sessionService, INavigationService navigationService)
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



            ApplyClampTrigger = ReactiveCommand.CreateFromTask(ApplyClamp);
            LoadLogsCommand = ReactiveCommand.CreateFromTask(LoadLogs);
            DeleteSessionCommand = ReactiveCommand.CreateFromTask(DeleteSession);


            DoubleClickCommand = ReactiveCommand.CreateFromTask<ParsedLog>(log =>
            OpenFullInfoPane(log)
            );



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