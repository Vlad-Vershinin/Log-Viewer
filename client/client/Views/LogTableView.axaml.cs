using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using System;

namespace client.Views;

public partial class LogTableView : UserControl
{
    public LogTableView()
    {
        InitializeComponent();
        DataContext = new LogTableViewModel();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}