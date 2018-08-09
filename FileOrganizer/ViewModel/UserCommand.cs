using FileOrganizer.Model;
using System;
using System.IO;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// ICommand implementation for the move command that moves a file to the specified location.
    /// </summary>
    class UserCommand : ICommand
    {
        private readonly Action<DirectoryInfo, CommandType> Action;
        private readonly DirectoryInfo Destination;
        private readonly CommandType CommandType;

        public event EventHandler CanExecuteChanged;

        public UserCommand(Action<DirectoryInfo, CommandType> action,
            DirectoryInfo destination,
            CommandType commandType = CommandType.MOVE)
        {
            Action = action;
            Destination = destination;
            CommandType = commandType;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
                Action(Destination, CommandType);
        }
    }
}
