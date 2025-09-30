using Avalonia.Controls;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views;

public partial class MainPageView : UserControl
{
    public MainPageView(MainPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}