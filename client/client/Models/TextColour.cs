using Avalonia.Media;
using ReactiveUI.Fody.Helpers;
using System;

namespace client.Models;

public class TextColour
{
    [Reactive] public bool IsHidden { get; set; } = false;
    [Reactive] public DateTime Time { get; set; }
    [Reactive] public string TimeStr { get { return $"{Time.ToShortTimeString()}"; } }
    [Reactive] public string Type { get; set; } = string.Empty;
    [Reactive] public string Content { get; set; } = string.Empty;

    // Свойство для цвета
    public SolidColorBrush BackgroundColor => GetColorByLogType(Type);

    private static SolidColorBrush GetColorByLogType(string logType)
    {
        return logType.ToLower() switch
        {
            "info" => new SolidColorBrush(Colors.LightBlue),
            "debug" => new SolidColorBrush(Colors.LightGreen),
            "trace" => new SolidColorBrush(Colors.LightGray),
            "warn" => new SolidColorBrush(Colors.LightYellow),
            "error" => new SolidColorBrush(Colors.LightCoral),
            _ => new SolidColorBrush(Colors.White)
        };
    }
}
