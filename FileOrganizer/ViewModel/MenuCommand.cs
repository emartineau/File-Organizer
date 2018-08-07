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
        private Action<OrganizerVM> action;
        public event EventHandler CanExecuteChanged;

        public MenuCommand(OrganizerVM organizerVM, Action<OrganizerVM> action)
        {
            this.organizerVM = organizerVM;
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(organizerVM);
        }
    }
}
