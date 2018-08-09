using System.Windows;
using System.Windows.Controls;
using FileOrganizer.ViewModel;

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
    }
}
