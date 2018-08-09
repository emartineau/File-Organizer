using FileOrganizer.Model;
using FileOrganizer.ViewModel.AppCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// Application ViewModel. Contains Commands and bindings.
    /// </summary>
    public class OrganizerVM : INotifyPropertyChanged
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
                OnPropertyChanged("WorkingFiles");
            }
        }
        public IList<FileSystemInfo> WorkingFiles
        {
            get => Organizer.WorkingFiles;
        }

        public FileSystemInfo CurrentFileSystemInfo
        {
            get => WorkingFiles[CurrentFileIndex];
            set
            {
                CurrentFileIndex = WorkingFiles.IndexOf(value);
                LastSelectedFile = CurrentFileSystemInfo;
            }
        }

        private FileSystemInfo _lastSelectedFile;
        public FileSystemInfo LastSelectedFile
        {
            get => _lastSelectedFile;
            set
            {
                if (value is FileInfo)
                {
                    _lastSelectedFile = value as FileInfo;
                    OnPropertyChanged("LastSelectedFile");
                }
            }
        }

        private int CurrentFileIndex
        {
            get => Organizer.CurrentFileIndex;
            set
            {
                if (WorkingFiles != null)
                {
                Organizer.CurrentFileIndex = value % WorkingFiles.Count;
                OnPropertyChanged("CurrentFile");
                }
            }
        }
        #endregion

        public ICommand ToParentDirectory { get; set; }
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

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ToParentDirectory = new MenuCommand(this, MenuCommandList.ToParent);
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
            catch (Exception e) when (e is IOException || e is DirectoryNotFoundException)
            {
                Console.WriteLine("File could not be read.");
                Console.WriteLine(e.Message);
            }

            foreach (var entry in KeyMap.ReadMapping(contents))
            {
                var command = new UserCommand(FileOperationAsync, entry.Value.Item1, entry.Value.Item2);
                var kb = new KeyBinding
                {
                    Command = command,
                    Key = entry.Key
                };
                KeyBindings.Add(kb);
            }
        }

        private async void FileOperationAsync(DirectoryInfo destination, CommandType commandType)
        {
            var movingFile = CurrentFileSystemInfo;

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
            var sourcePath = movingFile.FullName;
            var destPath = Path.Combine(destination.FullName, movingFile.Name);
            var del = Commands.Delegates[commandType];

            await Task.Run(() => del(sourcePath, destPath));
            }
            catch (Exception e) when (e is IOException || e is DirectoryNotFoundException)
            {
                Console.WriteLine($"Could not move file {movingFile.Name} to {destination.FullName}");
                Console.WriteLine(e.Message);
            }

            var completionMessage = $"Performed operation {commandType.ToString()} on File:{movingFile.Name}, Destination:{destination.FullName}.";
            Console.WriteLine(completionMessage);
        }
    }
}
