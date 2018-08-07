using FileOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static FileOrganizer.Model.AppCommands;

namespace FileOrganizer.ViewModel
{
    class MenuCommand : ICommand
    {
        private readonly AppDel appDel;
        private Organizer organizer;

        public event EventHandler CanExecuteChanged;

        public MenuCommand(AppDel appDel, Organizer organizer)
        {
            this.appDel = appDel;
            this.organizer = organizer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            appDel(ref organizer);
        }
    }
}
