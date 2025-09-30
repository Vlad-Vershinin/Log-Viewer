using Avalonia.Controls;
using client.services.interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    public class NavigationService : ReactiveObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<UserControl> _navigationStack;


        public NavigationService(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
            _navigationStack = new Stack<UserControl>();
        }

        public bool CanGoBack => _navigationStack.Count > 0;

        public UserControl CurrentUserControl { get; set; }

        public void GoBack()
        {
            if (!CanGoBack)
                return;

            CurrentUserControl = _navigationStack.Pop();
            this.RaisePropertyChanged(nameof(CurrentUserControl));
        }

        public void NavigateTo<T>() where T : UserControl
        {
            NavigateTo(typeof(T));
        }

        public void NavigateTo(Type windowType)
        {
            if (!typeof(UserControl).IsAssignableFrom(windowType))
                throw new ArgumentException("Type must be a UserControl");

            if (CurrentUserControl != null)
            {
                _navigationStack.Push(CurrentUserControl);
            }

            CurrentUserControl = (UserControl)_serviceProvider.GetService(windowType);

            if (CurrentUserControl == null)
                throw new InvalidOperationException($"UserControl {windowType.Name} is not registered in DI container");

            this.RaisePropertyChanged(nameof(CurrentUserControl));
        }
    }
}
