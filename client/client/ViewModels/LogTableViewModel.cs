using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels
{
    

    public class LogTableViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> CreateBoardCommand { get; set; }

        public LogTableViewModel()
        {
            CreateBoardCommand = ReactiveCommand.Create(SwitchToDiagramPage);
        }


        private void SwitchToDiagramPage()
        {
            // Blank
        }


    }

}
