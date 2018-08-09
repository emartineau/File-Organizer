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
        private OrganizerVM OrganizerVM;
        private Action<OrganizerVM> Action;
        public event EventHandler CanExecuteChanged;

        public MenuCommand(OrganizerVM organizerVM, Action<OrganizerVM> action)
        {
            OrganizerVM = organizerVM;
            Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action(OrganizerVM);
        }
    }
}
