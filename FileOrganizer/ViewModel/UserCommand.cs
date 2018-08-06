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
        private Action<DirectoryInfo, CommandType> action;
        private DirectoryInfo destination;
        private CommandType commandType;

        public event EventHandler CanExecuteChanged;

        public UserCommand(Action<DirectoryInfo, CommandType> action,
            DirectoryInfo destination,
            CommandType commandType = CommandType.MOVE)
        {
            this.action = action;
            this.destination = destination;
            this.commandType = commandType;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
                action(destination, commandType);
        }
    }
}
