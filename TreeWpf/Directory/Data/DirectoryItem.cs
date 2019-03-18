using TreeWpf.Directory.Data;


namespace TreeWpf
{
    /// <summary>
    /// Information about the directory item such as a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        public DirectoryItemType Type { get; set; }

        public string FullPath { get; set; }

        /// <summary>
        /// The name of the Directory Item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
