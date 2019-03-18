using System.Collections.ObjectModel;
using System.Linq;


namespace TreeWpf.Directory.ViewModels
{
    /// <summary>
    /// The view model for the applications main Directory view
    /// </summary>
    class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();
            // Create the viewModels from data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                            children.Select(drive => new DirectoryItemViewModel(drive.FullPath, Data.DirectoryItemType.Drive)));
        }
        #endregion
    }

}
