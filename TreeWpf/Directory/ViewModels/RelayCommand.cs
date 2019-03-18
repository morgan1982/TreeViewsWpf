
using System;
using System.Windows.Input;

namespace TreeWpf.Directory.ViewModels
{
    /// <summary>
    /// A basic command that runs an action The relay will simply run its function is imported to the constructor
    /// </summary>
    class RelayCommand : ICommand
    {

        #region Private Members
        /// <summary>
        /// The action to run
        /// </summary>
        private Action _action;
        #endregion

        /// <summary>
        /// The event that is fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #region Constructor
        public RelayCommand(Action action)
        {
            _action = action;
        }
        #endregion
        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }
        #endregion
    }
}
