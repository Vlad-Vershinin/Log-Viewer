using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CommandToLogIn { get; set; }


        public LoginViewModel()
        {
            CommandToLogIn = ReactiveCommand.Create(LoginToNextPage);
        }


        private void LoginToNextPage()
        {
            // Blank, change 'tis method for it to switch to the next page. (Next page is LogTableView)
        }
    }
}
