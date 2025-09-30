using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;

namespace client.Views;

public partial class DiagramView : UserControl
{
    public DiagramView()
    {
        InitializeComponent();
        DataContext = new DiagramViewModel();
    }
}