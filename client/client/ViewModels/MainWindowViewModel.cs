using client.services.interfaces;

namespace client.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public INavigationService NavigationService => _navigationService;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
