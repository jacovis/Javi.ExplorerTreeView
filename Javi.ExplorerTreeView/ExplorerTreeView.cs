// -----------------------------------------------------------------------
// <copyright file="ExplorerTreeView.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using WSF;
    using WSF.Enums;
    using WSF.IDs;
    using WSF.Interfaces;

    /// <summary>
    /// This control displays a file system tree akin the navigation pane in the windows explorer application.
    /// Based on https://blog.khmylov.com/2010/11/18/wpf-explorer-treeview-with-selectedpath-binding/
    /// which inherits from https://www.codeproject.com/Articles/21248/A-Simple-WPF-Explorer-Tree
    /// see also https://joshsmithonwpf.wordpress.com/2007/11/09/reaction-to-a-simple-wpf-explorer-tree/
    /// Uses the nuget package Dirkster.WSF to get all known folders from the windows shell.
    /// </summary>
    public class ExplorerTreeView : TreeView
    {
        #region Dependency properties

        public string SelectedFolder
        {
            get
            {
                return (string)GetValue(SelectedFolderProperty);
            }
            set
            {
                SetValue(SelectedFolderProperty, value);
            }
        }
        public static readonly DependencyProperty SelectedFolderProperty = DependencyProperty
            .Register("SelectedFolder", typeof(string), typeof(ExplorerTreeView));

        public bool UnloadItemsOnCollapse
        {
            get
            {
                return (bool)GetValue(UnloadItemsOnCollapseProperty);
            }
            set
            {
                SetValue(UnloadItemsOnCollapseProperty, value);
            }
        }
        public static readonly DependencyProperty UnloadItemsOnCollapseProperty = DependencyProperty
            .Register("UnloadItemsOnCollapse", typeof(bool), typeof(ExplorerTreeView), new UIPropertyMetadata(true));

        public bool Sort
        {
            get
            {
                return (bool)GetValue(SortProperty);
            }
            set
            {
                SetValue(SortProperty, value);
            }
        }
        public static readonly DependencyProperty SortProperty = DependencyProperty
            .Register("Sort", typeof(bool), typeof(ExplorerTreeView), new UIPropertyMetadata(true));

        public ExplorerTreeViewRootEnum Root
        {
            get
            {
                return (ExplorerTreeViewRootEnum)GetValue(RootProperty);
            }
            set
            {
                SetValue(RootProperty, value);
            }
        }
        public static readonly DependencyProperty RootProperty = DependencyProperty
            .Register("Root", typeof(ExplorerTreeViewRootEnum), typeof(ExplorerTreeView),
                      new UIPropertyMetadata(ExplorerTreeViewRootEnum.Desktop,
                                             (o, e) => ((ExplorerTreeView)o).InitExplorer()));
        #endregion

        #region fields
        private bool IsExplorerTreeViewLoaded = false;
        #endregion

        /// <summary>
        /// This event is raised if an error occurs while creating the file system tree.
        /// </summary>
        public event EventHandler<ExplorerTreeViewErrorEventArgs> ExplorerError;

        /// <summary>
        /// Invocator for <see cref="ExplorerError"/> event.
        /// </summary>
        /// <param name="e"></param>
        private void InvokeExplorerError(ExplorerTreeViewErrorEventArgs e)
        {
            ExplorerError?.Invoke(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplorerTreeView"/> class.
        /// </summary>
        public ExplorerTreeView()
        {
            Loaded += ExplorerTreeView_Loaded;

            SelectedItemChanged += OnSelectedItemChanged;

            AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnItemExpanded));
            AddHandler(TreeViewItem.CollapsedEvent, new RoutedEventHandler(OnItemCollapsed));
        }

        private void ExplorerTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.IsExplorerTreeViewLoaded)
            {
                this.IsExplorerTreeViewLoaded = true;
                InitExplorer();
            }
        }

        /// <summary>
        /// This method is invoked when user selects a node.
        /// It causes <see cref="SelectedFolder"/> to update its value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            SelectedFolder = GetSelectedFolder();
        }

        /// <summary>
        /// Occurs when tree node is expanded.
        /// Reloads node sub-folders, if required.
        /// May raise <see cref="ExplorerError"/> on some IO exceptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)e.OriginalSource;

            if (UnloadItemsOnCollapse || !HasSubFolders(item))
            {
                item.Items.Clear();
                IterateFolders(item.Tag as IDirectoryBrowser, item.Items);
            }
        }

        /// <summary>
        /// Occurs when tree node is collapsed.
        /// Unloads node sub-folders, if <see cref="UnloadItemsOnCollapse"/> is set to True.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCollapsed(object sender, RoutedEventArgs e)
        {
            if (UnloadItemsOnCollapse)
            {
                var item = (TreeViewItem)e.OriginalSource;
                item.Items.Clear();
                item.Items.Add("*");
            }
        }

        /// <summary>
        /// Checks whether specified <see cref="TreeViewItem"/> has any real sub-folder nodes.
        /// </summary>
        /// <param name="item">Node to check.</param>
        /// <returns></returns>
        private static bool HasSubFolders(TreeViewItem item)
        {
            if (item.Items.Count == 0)
            {
                return false;
            }

            return item.Items[0] is TreeViewItem;
        }

        private static TreeViewItem CreateItem(Object tag, string header)
        {
            var item = new TreeViewItem
            {
                Tag = tag,
                Header = header
            };

            item.Items.Add("*");

            return item;
        }

        /// <summary>
        /// Populates tree with initial drive nodes. 
        /// </summary>
        public void InitExplorer()
        {
            Items.Clear();

            IDirectoryBrowser root;
            switch (this.Root)
            {
                case ExplorerTreeViewRootEnum.MyComputer:
                    root = Browser.Create(KF_IID.ID_FOLDERID_ComputerFolder);
                    break;
                case ExplorerTreeViewRootEnum.Documents:
                    root = Browser.Create(KF_IID.ID_FOLDERID_Documents);
                    break;
                case ExplorerTreeViewRootEnum.Downloads:
                    root = Browser.Create(KF_IID.ID_FOLDERID_Downloads);
                    break;
                case ExplorerTreeViewRootEnum.Music:
                    root = Browser.Create(KF_IID.ID_FOLDERID_Music);
                    break;
                case ExplorerTreeViewRootEnum.Videos:
                    root = Browser.Create(KF_IID.ID_FOLDERID_Videos);
                    break;
                case ExplorerTreeViewRootEnum.Desktop:
                default:
                    root = Browser.Create(KF_IID.ID_FOLDERID_Desktop);
                    break;
            }

            var rootItem = CreateItem(root, root.Name);
            Items.Add(rootItem);
            IterateFolders(root, rootItem.Items);

            rootItem.IsExpanded = true;
        }

        private void IterateFolders(IDirectoryBrowser browser, ItemCollection items)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                try
                {
                    var childItems = new List<IDirectoryBrowser>();
                    foreach (var item in Browser.GetChildItems(browser.PathShell))
                    {
                        childItems.Add(item);
                    }
                    if (this.Sort && browser.SpecialPathId != KF_IID.ID_FOLDERID_Desktop)
                    {
                        childItems.Sort((x, y) => this.SortIDirectoryBrowser(x, y));
                    }
                    foreach (var item in childItems)
                    {
                        items.Add(CreateItem(item, item.Label));
                    }
                }
                catch (Exception ex)
                {
                    InvokeExplorerError(new ExplorerTreeViewErrorEventArgs(ex));
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private int SortIDirectoryBrowser(IDirectoryBrowser x, IDirectoryBrowser y)
        {
            CompareStringNatural compareStringNatural = new CompareStringNatural();
            bool xIsDrive = IsDrive(x);
            bool yIsDrive = IsDrive(y);
            if (xIsDrive && yIsDrive)
            {
                return compareStringNatural.Compare(x.PathFileSystem, y.PathFileSystem);
            }
            else if (xIsDrive)
            {
                return 1;
            }
            else if (yIsDrive)
            {
                return -1;
            }
            else
            {
                return compareStringNatural.Compare(x.Label, y.Label);
            }
        }

        private bool IsDrive(IDirectoryBrowser directoryBrowser)
        {
            try
            {
                if ((directoryBrowser.ItemType & DirectoryItemFlags.Drive) == DirectoryItemFlags.Drive) { return true; }
                if (directoryBrowser.PathFileSystem != null && new DirectoryInfo(directoryBrowser.PathFileSystem).Parent == null) { return true; }
            }
            catch (Exception)
            {
                // ignore
            }

            return false;
        }

        /// <summary>
        /// Returns full path of the selected node.
        /// </summary>
        /// <returns></returns>
        private string GetSelectedFolder()
        {
            var item = (TreeViewItem)SelectedItem;
            if (item == null) { return null; }

            IDirectoryBrowser directoryBrowser = item.Tag as IDirectoryBrowser;
            if ((directoryBrowser.ItemType & DirectoryItemFlags.FileSystemDirectory) == DirectoryItemFlags.FileSystemDirectory)
            {
                return directoryBrowser.PathFileSystem;
            }
            else
            {
                if (PInvoke.SHParseDisplayName(directoryBrowser.ParseName, IntPtr.Zero, out IntPtr pidl, 0, out uint dummy) == 0)
                {
                    var path = new StringBuilder(260);
                    if (PInvoke.SHGetPathFromIDListW(pidl, path))
                    {
                        return path.ToString();
                    }
                }
            }

            return null;
        }
    }
}