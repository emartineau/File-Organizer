using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileOrganizer.View.UIFrames
{
    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : Page
    {
        public FileView()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            element?.Focus();
        }
    }
}
