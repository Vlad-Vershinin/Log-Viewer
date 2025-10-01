using client.services.interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CommandToLogIn { get; set; }

        private readonly INavigationService _navigationService;

        public HttpClient _HttpClient { get; set; }


        public LoginViewModel(INavigationService navigationService)
        {
            CommandToLogIn = ReactiveCommand.Create(LoginToNextPage);

            _navigationService = navigationService;
        }


        private void LoginToNextPage()
        {

        }
    }
}
