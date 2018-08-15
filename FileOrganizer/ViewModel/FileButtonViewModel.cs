using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace FileOrganizer.ViewModel
{
    public class FileButtonViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public FileButtonViewModel(FileSystemInfo fileSystemInfo, Action OnClick)
        {
            FileSystemInfo = fileSystemInfo;
            Select = new RelayCommand(OnClick, () => true);
            IsSelected = false;
        }

        public FileSystemInfo FileSystemInfo { get; set; }
        public ICommand Select { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
