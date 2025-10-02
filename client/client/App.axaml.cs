using Avalonia;
using Avalonia.Markup.Xaml;
using client.services;
using client.services.interfaces;
using client.ViewModels;
using client.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace client
{
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        public static IServiceProvider? ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<LogTableViewModel>();
            services.AddTransient<DiagramViewModel>();
            services.AddTransient<LoginViewModel>();

            services.AddSingleton<SessionService>();

            _serviceProvider = services.BuildServiceProvider();
            ServiceProvider = _serviceProvider;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindowViewModel mainViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
            }


            base.OnFrameworkInitializationCompleted();
        }

    }
}