using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace client;

public partial class MainPageViewModel : UserControl
{
    public MainPageViewModel()
    {
        InitializeComponent();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}