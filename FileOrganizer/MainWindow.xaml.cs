using System.Windows;
using FileOrganizer.ViewModel;

namespace FileOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            OrganizerVM organizerVM = Application.Current.TryFindResource("VModel") as OrganizerVM;

            foreach (var binding in organizerVM?.KeyBindings)
            {
                InputBindings.Add(binding);
            }
            InitializeComponent();
        }
    }
}
