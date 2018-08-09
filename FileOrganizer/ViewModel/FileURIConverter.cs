using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace FileOrganizer.ViewModel
{
    [ValueConversion(typeof(FileSystemInfo), typeof(Uri))]
    class FileURIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            FileSystemInfo file = (FileSystemInfo)value;
            Uri convertedFile = new Uri(file.FullName);
            return convertedFile;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = (Uri)value;
            FileSystemInfo convertedURI = new FileInfo(uri.LocalPath);
            return convertedURI;
        }
    }
}
