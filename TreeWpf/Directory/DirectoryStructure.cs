using System;
using System.Collections.Generic;
using System.Linq;
using TreeWpf.Directory.Data;

namespace TreeWpf
{
    /// <summary>
    /// A helper class to query information about the directories
    /// </summary>
    public class DirectoryStructure
    {
        /// <summary>
        /// Get the logical Drives of the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // returns a list of the drives
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Get the directories top level content
        /// </summary>
        /// <param name="fullPath">the full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            //Create a list of Directory Items
            #region Get Directories
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
                }
            }
            catch (Exception) { }
            #endregion

            #region Get Files
            // Create a blank list of files
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
                }
            }
            catch (Exception) { }
            #endregion

            return items;
    }

    /// <summary>
    /// Finds the file or folder name from a full path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileFolderName(string path)
        {
            // C:\something\a folder
            if (string.IsNullOrEmpty(path))
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
