using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    class FSSingleClickCommand : ICommand
    {
        private OrganizerVM OrganizerVM;
        private Action<OrganizerVM, FileSystemInfo> Action;

        public event EventHandler CanExecuteChanged;

        public FSSingleClickCommand(OrganizerVM organizerVM)
        {
            OrganizerVM = organizerVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var fs = parameter as FileSystemInfo;
            OrganizerVM.CurrentFileSystemInfo = fs;
        }
    }
}
