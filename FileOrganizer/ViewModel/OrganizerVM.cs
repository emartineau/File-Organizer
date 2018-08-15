using FileOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// Application ViewModel. Contains Commands and bindings.
    /// </summary>
    public class OrganizerVM : INotifyPropertyChanged
    {
        private Organizer Organizer;
        private IList<FileSystemInfo> WorkingFiles { get => Organizer.WorkingFiles; }

        private FileInfo UserBindings;

        public OrganizerVM()
        {
            Organizer = new Organizer();
            KeyBindings = new List<KeyBinding>();

            var bindingConfig = Path.Combine(KeyMap.DefaultSavePath, KeyMap.DefaultFileName);
            UserBindings = new FileInfo(bindingConfig);

            // If a bindings file does not exist, create one.
            CheckBindings(UserBindings);

            // Uses the bindings file to fill the list with KeyBindings.
            CreateBindings(UserBindings);

            InitCommands();
            InitTimer();

            FileButtons = new ObservableCollection<FileButtonViewModel>();
            SetFileButtons();
        }

        #region INotifyProperty Properties
        public DirectoryInfo WorkingDirectory
        {
            get => Organizer.WorkingDirectory;
            set
            {
                Organizer.WorkingDirectory = value;
                SetFileButtons();
                OnPropertyChanged("WorkingDirectory");
                OnPropertyChanged("WorkingFiles");
                OnPropertyChanged("LastSelectedFile");
                OnPropertyChanged("FooterText");
            }
        }
        
        public ObservableCollection<FileButtonViewModel> FileButtons { get; }

        public FileButtonViewModel SelectedFileButton
        {
            get => FileButtons[CurrentFileIndex];
            set
            {
                if (value.FileSystemInfo.FullName == LastSelectedFile?.FullName) return;
                SelectedFileButton.IsSelected = false;

                CurrentFileIndex = FileButtons.ToList().FindIndex((fsi) =>
                    fsi.FileSystemInfo.FullName == value.FileSystemInfo.FullName);
                LastSelectedFile = SelectedFileButton.FileSystemInfo;

                SelectedFileButton.IsSelected = true;
            }
        }

        private FileSystemInfo _lastSelectedFile;
        public FileSystemInfo LastSelectedFile
        {
            get => _lastSelectedFile;
            set
            {
                if (File.Exists(value.FullName))
                {
                    _lastSelectedFile = new FileInfo(value.FullName);
                    OnPropertyChanged("LastSelectedFile");
                }
            }
        }

        public string TitleText { get => $"{SelectedFileButton.FileSystemInfo.Name} - File Organizer"; }
        public string FooterText
        {
            get =>
                  $"Current Folder: '{WorkingDirectory.Name}' " +
                  (WorkingFiles == null || WorkingFiles.Count == 0 ? "" :
                    $"Selected: '{SelectedFileButton.FileSystemInfo.Name}' [{CurrentFileIndex + 1}/{WorkingFiles.Count}]");
        }

        public int CurrentFileIndex
        {
            get => Organizer.CurrentFileIndex;
            private set
            {
                if (WorkingFiles != null)
                {
                    Organizer.CurrentFileIndex = value % WorkingFiles.Count;
                    LastSelectedFile = FileButtons[CurrentFileIndex].FileSystemInfo;
                    OnPropertyChanged("SelectedFileButton");
                    OnPropertyChanged("LastSelectedFile");
                    OnPropertyChanged("TitleText");
                    OnPropertyChanged("FooterText");
                }
            }
        }
        #endregion

        public ICommand ToParentDirectoryCommand { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICollection<KeyBinding> KeyBindings { get; private set; }
        public Timer ClickTimer { get; private set; }  // For distinguishing Single and Double Clicks

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitCommands()
        {
            ToParentDirectoryCommand = new RelayCommand(ToParentDirectory, () => true);
            OpenFileDialogCommand = new RelayCommand(OpenFileDialog, () => true);
        }

        private void ToParentDirectory()
        {
            WorkingDirectory = WorkingDirectory.Parent;
        }

        private void OpenFileDialog()
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Pick a folder to switch to.",
                ShowNewFolderButton = true
            };

            folderBrowserDialog.ShowDialog();

            if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                WorkingDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
            }
        }

        private void FileButtonOnClick(FileSystemInfo fsInfo)
        {
            if (ClickTimer.Enabled) // Double-Click
            {
                if (Directory.Exists(fsInfo.FullName) && fsInfo.FullName == SelectedFileButton.FileSystemInfo.FullName)
                {
                    WorkingDirectory = new DirectoryInfo(fsInfo.FullName);
                }
                else
                {
                    SelectedFileButton = FileButtons.ToList().Find((fb) => fb.FileSystemInfo.FullName == fsInfo.FullName);
                }
            }
            else // Single-Click
            {
                ClickTimer.Start();
                SelectedFileButton = FileButtons.ToList().Find((fb) => fb.FileSystemInfo.FullName == fsInfo.FullName);
            }
        }

        private void SetFileButtons()
        {
            FileButtons.Clear();
            foreach (var file in WorkingFiles)
            {
                FileButtons.Add(new FileButtonViewModel(file, () => FileButtonOnClick(file)));
            }
            OnPropertyChanged("FileButtons");
        }

        private void InitTimer()
        {
            ClickTimer = new Timer
            {
                Interval = 250 // up to 250ms to double click
            };
            ClickTimer.Elapsed += (sender, args) => ClickTimer.Stop();
        }

        private void CheckBindings(FileInfo bindingsFile)
        {
            if (!UserBindings.Exists)
            {
                var stream = UserBindings.CreateText();
                stream.Write(KeyMap.DefaultBinding);
                stream.Close();
            }
        }

        private void CreateBindings(FileInfo bindingsFile)
        {
            string contents = string.Empty;

            try
            {
                using (StreamReader bindingReader = new StreamReader(UserBindings.OpenRead()))
                {
                    contents = bindingReader.ReadToEnd();
                }
            }
            catch (Exception e) when (e is IOException || e is DirectoryNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine("File could not be read.");
                System.Diagnostics.Debug.WriteLine(e.Message);
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
            var movingFile = SelectedFileButton.FileSystemInfo;

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
                System.Diagnostics.Debug.WriteLine($"Could not move file {movingFile.Name} to {destination.FullName}");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            var completionMessage = $"Performed operation {commandType.ToString()} on File:{movingFile.Name}, Destination:{destination.FullName}.";
            System.Diagnostics.Debug.WriteLine(completionMessage);
        }
    }
}
