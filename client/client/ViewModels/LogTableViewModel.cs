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
using client.Models;

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




        [Reactive]
        public List<AssignedFileTest> FilesAssigned { get; set; }









        public LogTableViewModel()
        {

            FilesAssigned = new List<AssignedFileTest>();
            FilesAssigned.Add(new AssignedFileTest("ttt1"));
            FilesAssigned.Add(new AssignedFileTest("ttt2"));
            FilesAssigned.Add(new AssignedFileTest("ttt3"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));
            FilesAssigned.Add(new AssignedFileTest("ttt4"));







            CreateBoardCommand = ReactiveCommand.Create(SwitchToDiagramPage);
            OpenOptionPane = ReactiveCommand.CreateFromTask(OpenPane);


            NextPage = ReactiveCommand.CreateFromTask(ToNextPage);
            PreviousPage = ReactiveCommand.CreateFromTask(ToPreviousPage);
            LastPage = ReactiveCommand.CreateFromTask(ToLastPage);
            FirstPage = ReactiveCommand.CreateFromTask(ToFirstPage);



            ApplyClampTrigger = ReactiveCommand.CreateFromTask(ApplyClamp);


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


    }

}
