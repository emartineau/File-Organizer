using System;
using System.IO;

namespace FileOrganizer.ViewModel.AppCommands
{
    public static class MenuCommandList
    {
        public static readonly Action<OrganizerVM> ToParent = (vm) => vm.WorkingDirectory = vm.WorkingDirectory.Parent;

        public static readonly Action<OrganizerVM> OpenFileDialog = (vm) =>
        {
            // implementation as per Gat's documentation
            Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            Gat.Controls.OpenDialogViewModel opvm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            opvm.IsDirectoryChooser = true;
            bool? result = opvm.Show();
            if (result == true)
            {
                // Get selected file path
                vm.WorkingDirectory = new DirectoryInfo(opvm.SelectedFilePath);
            }
        };
    }
}
