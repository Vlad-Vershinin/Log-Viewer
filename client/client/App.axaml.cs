using Avalonia;
using Avalonia.Markup.Xaml;
using client.services;
using client.services.interfaces;
using client.ViewModels;
using client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace client
{
    public partial class App : Application
    {
        public static object ServiceProvider { get; internal set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var service = new ServiceCollection();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<LogTableViewModel>();


            ServiceProvider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindowViewModel mainViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
            }


            base.OnFrameworkInitializationCompleted();
        }

    }
}