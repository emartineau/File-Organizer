using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    class MenuCommand : ICommand
    {
        private OrganizerVM organizerVM;
        public event EventHandler CanExecuteChanged;

        public MenuCommand(OrganizerVM organizerVM)
        {
            this.organizerVM = organizerVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            organizerVM.WorkingDirectory = organizerVM.WorkingDirectory.Parent;
        }
    }
}
