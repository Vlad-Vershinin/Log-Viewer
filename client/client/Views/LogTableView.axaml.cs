using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using ReactiveUI;

namespace client.Views;

public partial class LogTableView : UserControl
{
    private LogTableViewModel logTableViewModel = new LogTableViewModel();
    public LogTableView()
    {
        InitializeComponent();
        logTableViewModel = new LogTableViewModel();
        DataContext = logTableViewModel;

        LogGrid.DoubleTapped += (s, e) =>
        {
            if (LogGrid.RowSelection != null)
            {
                logTableViewModel.DoubleClickCommand.Execute((ParsedLog)LogGrid.RowSelection.SelectedItem);
            }
        };


    }

}

