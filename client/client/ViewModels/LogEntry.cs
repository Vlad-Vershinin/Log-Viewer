using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class LogEntry : ReactiveObject
    {
        private bool _isHidden;
        public bool IsHidden
        {
            get => _isHidden;
            set => this.RaiseAndSetIfChanged(ref _isHidden, value);
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        private string _type = string.Empty;
        public string Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        private string _content = string.Empty;
        public string Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

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
}
