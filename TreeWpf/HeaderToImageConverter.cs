using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TreeWpf.Directory.Data;

namespace TreeWpf
{
    /// <summary>
    /// Codnvertd a full path to a specific image type of a drive folder or file
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // by default, we presume an image
            var image = "Images/closed_folder.png";

            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Images/drive.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Images/closed_folder.png";
                    break;
                case DirectoryItemType.File:
                    image = "Images/file.png";
                    break;
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
