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

namespace TreeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach(var drive in Directory.GetLogicalDrives())
            {
                // Create a new item from it
                var item = new TreeViewItem();
                item.Header = drive;
                item.Tag = drive;

                item.Items.Add(null);

                //Listen for an item being expanded
                item.Expanded += Folder_Expanded;

                FolderView.Items.Add(item);
            }
        }
        #region Folder Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender; // cast it to a TreeViewItem

            // if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            item.Items.Clear();

            var fullPath = (string)item.Tag;

            #region Find Directories
            // Create a blank list of directories
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch (Exception)
            {
                throw;
            }

            // for Each directory
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    // set Header as folder name 
                    Header = GetFileFolderName(directoryPath),
                    // Tag as full path
                    Tag = directoryPath

                };

                // add dummy item so we can expand the folder
                subItem.Items.Add(null);

                // handle the expanded items
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion

            #region Get Files
            // Create a blank list of files
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if(fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch (Exception)
            {
                throw;
            }

            // for Each directory
            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    // set Header as file name 
                    Header = GetFileFolderName(filePath),
                    // Tag as full path
                    Tag = filePath

                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });
        }
        #endregion
        #endregion
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full Path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // C:\something\a folder
            if(string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            // Make all slasahes back slashes
            var normalizedPath = path.Replace('/', '\\'); // uses single quotes cause \\ is actually a single char

            // find the last backslash of the path
            var lastIndex = normalizedPath.LastIndexOf('\\');
            // if there is no backslash return the path it self
            if (lastIndex <= 0)
                return path;

            // return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }
    }
}
