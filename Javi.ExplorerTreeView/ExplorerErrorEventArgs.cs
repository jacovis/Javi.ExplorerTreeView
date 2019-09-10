// -----------------------------------------------------------------------
// <copyright file="ExplorerTreeViewErrorEventArgs.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;

    /// <summary>
    /// Provides data for explorer navigation exception handling.
    /// </summary>
    public class ExplorerTreeViewErrorEventArgs : EventArgs
    {
        public ExplorerTreeViewErrorEventArgs(Exception ex)
        {
            Exception = ex;
        }

        public Exception Exception { get; private set; }
    }
}