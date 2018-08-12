using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel.Testing
{
    public class FileButtonViewModel
    {
        public FileButtonViewModel(string s)
        {
            FilePath = s;
            Select = new RelayCommand(MyMethod, CanExecute);
        }

        private bool CanExecute()
        {
            return true;
        }

        private void MyMethod()
        {
            System.Diagnostics.Debug.WriteLine(FilePath);
        }

        public string FilePath { get; set; }
        public ICommand Select { get; private set; }
    }
}
