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
    class Organizer
    {
        #region Properties
        private DirectoryInfo _workingDirectory;
        public DirectoryInfo WorkingDirectory
        {   get => _workingDirectory;
            set =>_workingDirectory = value;
        }
        private IList<FileInfo> _workingFiles;
        public IList<FileInfo> WorkingFiles
        {
            get => _workingFiles;
            set => _workingFiles = value.Where(file => !file.Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden)).ToList();
        }
        private int _currentFileIndex;
        public int CurrentFileIndex
        {
            get => _currentFileIndex;
            set => _currentFileIndex = value;
        }
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

        private void Initialize()
        {
            WorkingFiles = new ObservableCollection<FileInfo>(WorkingDirectory.GetFiles());
        }
    }
}
