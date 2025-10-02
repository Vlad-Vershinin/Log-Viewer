using Avalonia.Controls;
using client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();
        }
    }
}