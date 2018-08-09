using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        {
            get => _workingDirectory;
            set
            {
                if (value == null) return;
                _workingDirectory = value;
                WorkingFiles = WorkingDirectory.GetFileSystemInfos()
                    .Where(file => !file.Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden)).ToList();
                CurrentFileIndex = 0;
            }
        }
        public IList<FileSystemInfo> WorkingFiles { get; set; }
        public int CurrentFileIndex { get; set; }
        #endregion

        private readonly string DefaultWDPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // Constructor if no file is given
        public Organizer()
        {
            WorkingDirectory = new DirectoryInfo(DefaultWDPath);
        }

        // Program begins in given directory
        public Organizer(DirectoryInfo directoryInfo)
        {
            WorkingDirectory = directoryInfo;
        }
    }
}
