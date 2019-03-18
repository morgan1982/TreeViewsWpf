using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWpf.Directory.ViewModels
{
    /// <summary>
    /// A base view model that fires PropertyChanged events as needed
    /// </summary>
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
    }
}
