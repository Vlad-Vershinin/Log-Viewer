using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;

namespace client.Views;

public partial class LogTableView : UserControl
{
    private LogTableViewModel logTableViewModel = App.ServiceProvider.GetService<LogTableViewModel>();
    public LogTableView()
    {
        InitializeComponent();
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

