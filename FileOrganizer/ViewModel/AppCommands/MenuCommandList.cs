using System;
using System.IO;

namespace FileOrganizer.ViewModel.AppCommands
{
    public static class MenuCommandList
    {
        public static readonly Action<OrganizerVM> ToParent = (vm) => vm.WorkingDirectory = vm.WorkingDirectory.Parent;

        public static readonly Action<OrganizerVM> OpenFileDialog = (vm) =>
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Pick a folder to switch to.",
                ShowNewFolderButton = true
            };

            folderBrowserDialog.ShowDialog();
            vm.WorkingDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
        };
    }
}
