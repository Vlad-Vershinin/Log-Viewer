using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace client;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<MainPageViewModel>();
    }
}