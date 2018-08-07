using System;
using Gat.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileOrganizer.ViewModel.AppCommands
{
    public static class MenuCommandList
    {
        public static readonly Action<OrganizerVM> ToParent = (vm) => vm.WorkingDirectory = vm.WorkingDirectory.Parent;
    }
}
