﻿using FileOrganizer.ViewModel;
using System;
using System.Collections.Generic;
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

namespace FileOrganizer.View
{
    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : Page
    {
        public FileView()
        {
            OrganizerVM organizerVM = new OrganizerVM();

            foreach (var binding in organizerVM.KeyBindings)
            {
                InputBindings.Add(binding);
            }

            InitializeComponent();

            Binding file = new Binding("CurrentFile")
            {
                Source = organizerVM
            };

            FileDisplay.SetBinding(MediaElement.SourceProperty, file);
        }

        private void Grid_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
    }
}