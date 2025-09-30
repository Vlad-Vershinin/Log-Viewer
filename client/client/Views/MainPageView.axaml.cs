using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<MainPageViewModel>();
    }
}