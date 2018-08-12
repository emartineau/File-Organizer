using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    public class FileButtonViewModel
    {
        private readonly Timer ClickTimer; // For distinguishing Single and Double Clicks

        public FileButtonViewModel(FileSystemInfo fileSystemInfo, Action OnClick)
        {
            FileSystemInfo = fileSystemInfo;
            Select = new RelayCommand(OnClick, () => true);
        }

        public FileSystemInfo FileSystemInfo { get; set; }
        public ICommand Select { get; private set; }
    }
}
