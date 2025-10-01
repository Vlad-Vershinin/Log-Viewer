using client.services.interfaces;
using ReactiveUI;

namespace client.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        private readonly INavigationService _navigationService;

        public ViewModelBase(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }
    }
}
