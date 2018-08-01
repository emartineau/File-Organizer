using FileOrganizer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// Application ViewModel. Contains Commands and bindings.
    /// </summary>
    class OrganizerVM : INotifyPropertyChanged
    {
        private Organizer Organizer;

        #region INotifyProperty Properties
        public DirectoryInfo WorkingDirectory
        {
            get => Organizer.WorkingDirectory;
            set
            {
                Organizer.WorkingDirectory = value;
                OnPropertyChanged("WorkingDirectory");
            }
        }
        public IList<FileInfo> WorkingFiles
        {
            get => Organizer.WorkingFiles;
            set
            {
                Organizer.WorkingFiles = value;
                OnPropertyChanged("WorkingFiles");
            }
        }

        public FileInfo CurrentFile
        {
            get => WorkingFiles[CurrentFileIndex];
        }
        public int CurrentFileIndex
        {
            get => Organizer.CurrentFileIndex;
            set
            {
                Organizer.CurrentFileIndex = value % WorkingFiles.Count;
                OnPropertyChanged("CurrentFile");
            }
        }
        #endregion

        private ICommand _moveThisFile;
        public ICommand MoveThisFile { get => _moveThisFile; set => _moveThisFile = value; }
        public ICollection<KeyBinding> KeyBindings { get; set; }

        private FileInfo Bindings;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrganizerVM()
        {
            Organizer = new Organizer();
            KeyBindings = new List<KeyBinding>();

            var bindingConfig = Path.Combine(KeyMap.DefaultSavePath, KeyMap.DefaultFileName);
            Bindings = new FileInfo(bindingConfig);

            // If a bindings file does not exist, create one.
            CheckBindings(Bindings);

            // Uses the bindings file to fill the list with KeyBindings.
            CreateBindings(Bindings);

            Console.WriteLine(CurrentFile.Name);
        }

        private void CheckBindings(FileInfo bindingsFile)
        {
            if (!Bindings.Exists)
            {
                var stream = Bindings.CreateText();
                stream.Write(KeyMap.DefaultBinding);
                stream.Close();
            }
        }

        private void CreateBindings(FileInfo bindingsFile)
        {
            string contents = string.Empty;

            try
            {
                using (StreamReader bindingReader = new StreamReader(Bindings.OpenRead()))
                {
                    contents = bindingReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File could not be read.");
            }

            foreach (var entry in KeyMap.ReadMapping(contents))
            {
                var command = new MoveCommand(MoveFile, entry.Value);
                var kb = new KeyBinding
                {
                    Command = command,
                    Key = entry.Key
                };
                KeyBindings.Add(kb);
            }
        }

        private void MoveFile(DirectoryInfo destination)
        {
            // If the destination folder matches the identifying skip-name, skip moving this file.
            if (destination.Name == KeyMap.DefaultSkipName)
            {
                CurrentFileIndex++;
                return;
            }

            // If the destination folder does not exist: Nothing happens. (May auto-create folder depending on setting)
            if (!destination.Exists)
                return;

            try
            {
            var destPath = Path.Combine(destination.FullName, CurrentFile.Name);
            File.Move(CurrentFile.FullName, destPath);
            }
            catch (Exception e) when (e is IOException || e is DirectoryNotFoundException)
            {
                Console.WriteLine("Could not move file {0} to {1}", "Organizer.CurrentFile.Name", "destination.FullName");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Moved {0} to {1}.", CurrentFile.Name, destination.FullName);
        }
    }
}
