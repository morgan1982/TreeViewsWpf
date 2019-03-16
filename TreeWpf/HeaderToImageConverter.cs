using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeWpf
{
    /// <summary>
    /// Codnvertd a full path to a specific image type of a drive folder or file
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the full Path
            var path = (string)value;

            // if the path is null, ignore
            if (path == null)
                return null;

            // Get the name of the file / folder
            var name = MainWindow.GetFileFolderName(path);

            // by default, we presume an image
            var image = "Images/closed_folder.png";

            // if name is blank, we presume it's a drive
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/closed_folder.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
