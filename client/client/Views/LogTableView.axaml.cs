using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;

namespace client.Views;

public partial class LogTableView : UserControl
{
    public LogTableView()
    {
        InitializeComponent();
        DataContext = new LogTableViewModel();
        //LogGridGUI.ItemsSource = 
    }
}