using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace FileOrganizer.Model
{
    /**
     *  Central class for application logic.
     */
    class Organizer : INotifyPropertyChanged
    {
        private Dictionary<char, DirectoryInfo> keyMappings;
        public Dictionary<char, DirectoryInfo> KeyMappings { get => keyMappings; }

        #region INotifyProperty Properties
        public DirectoryInfo WorkingDirectory
        {   get => WorkingDirectory;
            set
            {
                WorkingDirectory = value;
                OnPropertyChanged("WorkingDirectory");
            }
        }
        public FileInfo CurrentFile
        {
            get => CurrentFile;
            set
            {
                CurrentFile = value;
                OnPropertyChanged("CurrentFile");
            }
        }
        public ObservableCollection<FileInfo> SelectableFiles
        {
            get => SelectableFiles;
            set
            { SelectableFiles = value;
                OnPropertyChanged("SelectableFiles");
            }
        }
        #endregion

        // Constructor if no file is given
        public Organizer()
        {
            WorkingDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            Initialize();
        }

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
            SelectableFiles = new ObservableCollection<FileInfo>(WorkingDirectory.GetFiles());
            CurrentFile = SelectableFiles.First();
        }
    }
}
