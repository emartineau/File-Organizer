using FileOrganizer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileOrganizer.View.UIFrames
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class Toolbar : Page
    {
        public Toolbar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var vm = button?.DataContext as OrganizerVM;
            Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            Gat.Controls.OpenDialogViewModel opvm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            opvm.IsDirectoryChooser = true;
            bool? result = opvm.Show();
            if (result == true)
            {
                // Get selected file path
                vm.WorkingDirectory = new DirectoryInfo(opvm.SelectedFilePath);
            }
        }
    }
}
