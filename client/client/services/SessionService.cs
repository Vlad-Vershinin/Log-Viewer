using client.ViewModels;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace client.services;

public class SessionService
{
    [Reactive] public string SessionName { get; set; } = string.Empty;

    public ObservableCollection<LogEntry>? Logs { get; set; } = [];

    public void Init(string sessionName)
    {
        SessionName = sessionName;
        Logs?.Clear();
    }

    public void CloseSession()
    {
        SessionName = string.Empty;
        Logs?.Clear();
    }
}
