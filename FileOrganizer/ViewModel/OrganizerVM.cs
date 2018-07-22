using FileOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// Application ViewModel. Contains Commands and bindings.
    /// </summary>
    class OrganizerVM
    {
        private Organizer Organizer;

        public ObservableCollection<FileInfo> FileList { get; set; }
        public FileInfo CurrentFile { get; set; }
        public int CurrentFileIndex { get; set; }

        private ICommand _moveThisFile;
        public ICommand MoveThisFile { get => _moveThisFile; set => _moveThisFile = value; }
        public IList<KeyBinding> KeyBindings { get; set; }

        private FileInfo Bindings;

        public OrganizerVM()
        {
            Organizer = new Organizer();
            FileList = new ObservableCollection<FileInfo>(Organizer.WorkingFiles);
            CurrentFileIndex = Organizer.CurrentFileIndex;
            CurrentFile = Organizer.CurrentFile;
            KeyBindings = new List<KeyBinding>();

            var bindingConfig = Path.Combine(KeyMap.DefaultSavePath, KeyMap.DefaultFileName);
            Bindings = new FileInfo(bindingConfig);

            // If a bindings file does not exist, create one.
            CheckBindings(Bindings);

            // Uses the given file to fill the list with KeyBindings.
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
            string contents = "";

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
            }
        }

        private void MoveFile(DirectoryInfo destination)
        {
            Console.WriteLine("MoveFile has executed.");
            // If the destination folder is blank: skip.
            if (!destination.Exists)
                return;

            if (destination.Name == KeyMap.DefaultSkipName)
            {
                CurrentFileIndex++;
                return;
            }
            try
            {
            File.Move(Organizer.CurrentFile.FullName, destination.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not move file {0} to {1}", "Organizer.CurrentFile.Name", "destination.FullName");
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine("Move to {0} performed successfully.", destination.FullName);
            CurrentFile = Organizer.WorkingFiles[CurrentFileIndex];
        }
    }
}
