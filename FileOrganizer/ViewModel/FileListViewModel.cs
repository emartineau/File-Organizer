using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer.ViewModel
{
    public class FileListViewModel
    {
        public FileListViewModel()
        {
            FileButtons = new ObservableCollection<FileButtonViewModel>();
        }

        public ObservableCollection<FileButtonViewModel> FileButtons;
    }
}
