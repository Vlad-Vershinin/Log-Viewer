using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views;

public partial class LogTableView : UserControl
{
    private LogTableViewModel logTableViewModel = new LogTableViewModel();
    public LogTableView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<LogTableViewModel>();
        LogGrid.DoubleTapped += (s, e) =>
        {
            if (LogGrid.RowSelection != null)
            {
                logTableViewModel.DoubleClickCommand.Execute((ParsedLog)LogGrid.RowSelection.SelectedItem);
            }
        };


    }
		  
    }    
}

}

