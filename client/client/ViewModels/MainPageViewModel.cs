using Avalonia.Controls.Documents;
using client.services.interfaces;
using client.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class MainPageViewModel
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public INavigationService NavigationService => _navigationService;
    }
}
