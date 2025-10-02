using Avalonia;
using Avalonia.Markup.Xaml;
using client.services;
using client.services.interfaces;
using client.ViewModels;
using client.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Avalonia.Controls.ApplicationLifetimes;

namespace client;

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

        // add services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<HttpClientService>();
        services.AddSingleton<SessionService>();

        // add views
        services.AddTransient<LoginView>();
        services.AddTransient<LogTableView>();

        // add view models
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LogTableViewModel>();
        services.AddTransient<DiagramViewModel>();
        services.AddTransient<LoginViewModel>();

        // add windows
        services.AddSingleton<MainWindow>();
            services.AddSingleton<SessionService>();

        _serviceProvider = services.BuildServiceProvider();
        ServiceProvider = _serviceProvider;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindowViewModel mainViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = ServiceProvider.GetService<MainWindow>();
            desktop.MainWindow.DataContext = mainViewModel;

            ServiceProvider.GetService<INavigationService>().NavigateTo<LoginView>();
        }
        base.OnFrameworkInitializationCompleted();
    }
}