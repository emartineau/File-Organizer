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
    /// Interaction logic for FileList.xaml
    /// </summary>
    public partial class FileList : Page
    {
        OrganizerVM organizerVM;

        public FileList()
        {
            organizerVM = Application.Current.TryFindResource("VModel") as OrganizerVM;

            foreach (var binding in organizerVM?.KeyBindings)
            {
                InputBindings.Add(binding);
            }
            InitializeComponent();
        }

        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            organizerVM.CurrentFileSystemInfo = GetItem(sender);
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fs = GetItem(sender);
            organizerVM.CurrentFileSystemInfo = fs;

            if (fs is DirectoryInfo)
            {
                organizerVM.WorkingDirectory = fs as DirectoryInfo;
            }
        }

        private FileSystemInfo GetItem(object sender)
        {
            var lb = sender as Button;
            return lb?.DataContext as FileSystemInfo;
        }
    }
}
