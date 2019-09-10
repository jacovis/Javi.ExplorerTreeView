// -----------------------------------------------------------------------
// <copyright file="ExplorerTreeViewImageConverter.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using WSF.Interfaces;

    public class ExplorerTreeViewImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewItem node)
            {
                IDirectoryBrowser directoryBrowser = node.Tag as IDirectoryBrowser;
                try
                {
                    ImageSource result = null;
                    if (result == null)
                    {
                        result = IconExtractor.GetIconSmall(directoryBrowser.PathFileSystem).ToImageSource();
                    }
                    if (result == null)
                    {
                        result = IconExtractor.GetIconSmall(directoryBrowser.SpecialPathId).ToImageSource();
                    }
                    if (result == null)
                    {
                        if (PInvoke.SHParseDisplayName(directoryBrowser.ParseName, IntPtr.Zero, out IntPtr pidl, 0, out uint dummy) == 0)
                        {
                            result = IconExtractor.GetIconSmall(pidl).ToImageSource();
                        }
                    }
                    if (result == null && directoryBrowser.IconResourceId != null)
                    {
                        string[] resourceId = directoryBrowser.IconResourceId.Split(',');
                        if (resourceId.Length == 2 && int.TryParse(resourceId[1], out int iconIndex))
                        {
                            if (string.IsNullOrEmpty(resourceId[0]) == false)
                            {
                                result = IconExtractor.GetIconSmall(resourceId[0], iconIndex).ToImageSource();
                            }
                        }
                    }

                    return result;
                }
                catch (Exception) {  }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
