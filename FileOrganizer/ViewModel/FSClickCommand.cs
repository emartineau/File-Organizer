using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    class FSClickCommand : ICommand
    {
        private OrganizerVM OrganizerVM;
        private readonly Timer ClickTimer; // For distinguishing Single and Double Clicks

        public event EventHandler CanExecuteChanged;

        public FSClickCommand(OrganizerVM organizerVM)
        {
            OrganizerVM = organizerVM;

            ClickTimer = new Timer
            {
                Interval = 250
            };
            ClickTimer.Elapsed += (sender, args) => ClickTimer.Stop();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var fs = parameter as FileSystemInfo;
            if (ClickTimer.Enabled) // Double-Click
            {
                if (fs is DirectoryInfo)
                {
                    OrganizerVM.WorkingDirectory = fs as DirectoryInfo;
                }
                else
                {
                    OrganizerVM.CurrentFileSystemInfo = fs;
                }
            }
            else // Single-Click
            {
                ClickTimer.Start();
                OrganizerVM.CurrentFileSystemInfo = fs;
            }
        }
    }
}
