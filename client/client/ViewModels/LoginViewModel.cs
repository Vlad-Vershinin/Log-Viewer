using client.services;
using client.services.interfaces;
using client.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Threading.Tasks;

namespace client.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly HttpClientService _httpClient;
    private readonly SessionService _sessionService;

    [Reactive] public string SessionName { get; set; } = string.Empty;

    public ReactiveCommand<Unit, Unit> CommandToLogIn { get; set; }

    public LoginViewModel(INavigationService navigationService, HttpClientService clientService, SessionService sessionService)
    {
        _navigationService = navigationService;
        _httpClient = clientService;
        _sessionService = sessionService;

        CommandToLogIn = ReactiveCommand.CreateFromTask(Login);
    }

    private async Task Login()
    {
        var res = await _httpClient.HttpClient.PostAsync($"session/connect/{SessionName}", null);

        if(res.IsSuccessStatusCode)
        {
            _sessionService.Init(SessionName);
            _navigationService.NavigateTo<LogTableView>();
        }
    }
}
