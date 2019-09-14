using Javi.ExplorerTreeView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Demo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        /// <summary>
        /// Gets the possible root selection options as a list.
        /// </summary>
        public IList<ExplorerTreeViewRootEnum> RootSelections
        {
            get
            {
                return Enum.GetValues(typeof(ExplorerTreeViewRootEnum)).Cast<ExplorerTreeViewRootEnum>().ToList<ExplorerTreeViewRootEnum>();
            }
        }

        private ExplorerTreeViewRootEnum rootSelection;
        /// <summary>
        /// Gets or sets the selected root.
        /// </summary>
        public ExplorerTreeViewRootEnum RootSelection
        {
            get
            {
                return this.rootSelection;
            }
            set
            {
                if (value != this.rootSelection)
                {
                    this.rootSelection = value;
                    NotifyPropertyChanged(nameof(RootSelection));
                }
            }
        }

        private string selectedFolder;
        /// <summary>
        /// The selected folder.
        /// </summary>
        public string SelectedFolder
        {
            get
            {
                return this.selectedFolder;
            }
            set
            {
                if (value != this.selectedFolder)
                {
                    this.selectedFolder = value;
                    NotifyPropertyChanged(nameof(SelectedFolder));
                }
            }
        }
    }
}
