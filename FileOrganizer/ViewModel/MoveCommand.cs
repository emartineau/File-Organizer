using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileOrganizer.ViewModel
{
    /// <summary>
    /// ICommand implementation for the move command that moves a file to the specified location.
    /// </summary>
    class MoveCommand : ICommand
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
                move(destination);
        }
    }
}
