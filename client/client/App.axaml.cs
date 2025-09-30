using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using client.services;
using client.services.interfaces;
using client.ViewModels;
using client.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            service.AddSingleton(typeof(INavigationService), typeof(NavigationService));


            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new LogTableViewModel()
                };
            }


            base.OnFrameworkInitializationCompleted();
        }

    }
}