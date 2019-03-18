using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TreeWpf.Directory.Data;

namespace TreeWpf.Directory.ViewModels
{
    class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// the type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path of the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        // ObservableCollection is just a list that fires an event when items are added or removed from the list
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if the item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                    Expand();
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Public Commands
        /// <summary>
        /// Command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; } // a type of comand is needed to run the comand that's why we need the relay 
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="fullPath">the FullPath of the file</param>
        /// <param name="type">The type of the item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.FullPath = fullPath;
            this.Type = type;

            this.ClearChildren();
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Removes the children from the list adding a dummy item 
        /// </summary>
        private void ClearChildren()
        {
            // Clears the children
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not in a file
            if (this.Type != DirectoryItemType.File)
            {
                this.Children.Add(null);
            }
        }
        #endregion

        /// <summary>
        /// Expands the directory and finds all the children
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            // Find all Chilren
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
           

            
        }
    }
}
