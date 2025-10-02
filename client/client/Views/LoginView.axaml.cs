using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using System;

namespace client.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<LoginViewModel>();
    }

    private void InitializeComponent()
    {
        
    }
}