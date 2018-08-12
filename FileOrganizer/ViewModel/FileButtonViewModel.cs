using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    class FileButtonViewModel
    {
        public FileButtonViewModel(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }

        public FileSystemInfo FileSystemInfo { get; set; }
        public ICommand Select { get; private set; }
    }
}
