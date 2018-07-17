using FileOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    class OrganizerVM
    {
        public Organizer Organizer { get; set; }
        public ObservableCollection<FileInfo> FileList { get; set; }

        private ICommand moveThisFile;
        public ICommand MoveThisFile { get => moveThisFile; set => moveThisFile = value; }
        public IList<KeyBinding> KeyBindings { get; set; }

        public OrganizerVM()
        {
            Organizer = new Organizer();
            FileList = new ObservableCollection<FileInfo>(Organizer.SelectableFiles);
            KeyBindings = new List<KeyBinding>();

            foreach (var entry in Organizer.KeyMappings)
            {
                var command = new MoveCommand(MoveFile, entry.Value));
                var kb = new KeyBinding();
                kb.Command = command;

            }
        }

        private void MoveFile(DirectoryInfo destination)
        {
            File.Move(Organizer.CurrentFile.FullName, destination.FullName);
        }

        private class MoveCommand : ICommand
        {
            private Action<DirectoryInfo> move;
            private DirectoryInfo destination;

            public event EventHandler CanExecuteChanged;

            public MoveCommand(Action<DirectoryInfo> action, DirectoryInfo destination)
            {
                move = action;
                this.destination = destination;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                if (parameter != null)
                {
                    move(destination);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
