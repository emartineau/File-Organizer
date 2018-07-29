using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FileOrganizer.ViewModel
{
    [ValueConversion(typeof(FileInfo), typeof(Uri))]
    class FileURIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FileInfo file = (FileInfo)value;
            Uri convertedFile = new Uri(file.FullName);
            return convertedFile;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = (Uri)value;
            FileInfo convertedURI = new FileInfo(uri.LocalPath);
            return convertedURI;
        }
    }
}
