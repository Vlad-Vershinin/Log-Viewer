using client.services;
using client.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class DiagramViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public DiagramViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
