using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace FileOrganizer.Model
{
    /// <summary>
    /// Central class for application logic.
    /// </summary>
    class Organizer : INotifyPropertyChanged
    {
        #region INotifyProperty Properties
        private DirectoryInfo _workingDirectory;
        public DirectoryInfo WorkingDirectory
        {   get => _workingDirectory;
            set
            {
                _workingDirectory = value;
                OnPropertyChanged("WorkingDirectory");
            }
        }
        private FileInfo _currentFile;
        public FileInfo CurrentFile
        {
            get => _currentFile;
            set
            {
                _currentFile = value;
                OnPropertyChanged("CurrentFile");
            }
        }
        private ObservableCollection<FileInfo> _selectableFiles;
        public ObservableCollection<FileInfo> WorkingFiles
        {
            get => _selectableFiles;
            set
            {
                _selectableFiles = value;
                OnPropertyChanged("SelectableFiles");
            }
        }
        public int CurrentFileIndex { get; set; } = 0;
        #endregion

        private string DefaultWDPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // Constructor if no file is given
        public Organizer()
        {
            WorkingDirectory = new DirectoryInfo(DefaultWDPath);
            Initialize();
        }

        // Program begins in given directory
        public Organizer(DirectoryInfo directoryInfo)
        {
            WorkingDirectory = directoryInfo;
            Initialize();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Initialize()
        {
            WorkingFiles = new ObservableCollection<FileInfo>(WorkingDirectory.GetFiles());
            CurrentFile = WorkingFiles[CurrentFileIndex];
        }
    }
}
