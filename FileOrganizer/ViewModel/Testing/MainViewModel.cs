using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer.ViewModel.Testing
{
    class MainViewModel
    {
        public MainViewModel()
        {
            Records = new ObservableCollection<FileButtonViewModel>();
            Records.Add(new FileButtonViewModel("Hello"));
        }

        public ObservableCollection<FileButtonViewModel> Records { get; }
    }
}
