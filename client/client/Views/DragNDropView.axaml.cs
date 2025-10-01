using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using ReactiveUI;

namespace client.Views;

public partial class DragNDropView : UserControl
{
    public DragNDropView()
    {
        InitializeComponent();
    }
    public void OnDrop(object sender, DragEventArgs e)
    {
        
    }
}