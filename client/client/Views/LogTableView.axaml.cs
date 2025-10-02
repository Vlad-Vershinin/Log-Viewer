using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views;

public partial class LogTableView : UserControl
{
    public LogTableView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<LogTableViewModel>();
		  
    }    
}


