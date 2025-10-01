using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services.interfaces
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : UserControl;
        void NavigateTo(Type windowType);
        void GoBack();
        bool CanGoBack { get; }
        UserControl CurrentUserControl { get; }
    }
}
