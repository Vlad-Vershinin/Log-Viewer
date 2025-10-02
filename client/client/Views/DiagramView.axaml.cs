using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace client.Views;

public partial class DiagramView : UserControl
{
    public DiagramView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetService<DiagramViewModel>();
    }
}