// -----------------------------------------------------------------------
// <copyright file="IconExtractor.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Helper class for extracting icons for file or folder from Window file system.
    /// </summary>
    public class IconExtractor
    {
        /// <summary>
        /// Gets the small icon for the specified file or folder.
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconSmall(string fileName)
        {
            return GetIcon(fileName, PInvoke.SHGFI_SMALLICON);
        }

        /// <summary>
        /// Gets the large icon for the specified file or folder.
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconLarge(string fileName)
        {
            return GetIcon(fileName, PInvoke.SHGFI_LARGEICON);
        }

        private static Icon GetIcon(string fileName, uint flags = 0)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { return null; }

            PInvoke.SHFILEINFO shi = new PInvoke.SHFILEINFO();
            flags = flags | PInvoke.SHGFI_ICON | PInvoke.SHGFI_ADDOVERLAYS;
            IntPtr hIcon = PInvoke.SHGetFileInfo(fileName, 0, out shi, (uint)(Marshal.SizeOf(shi)), flags);

            if (shi.hIcon == IntPtr.Zero) { return null; }

            Icon icon = (Icon)Icon.FromHandle(shi.hIcon).Clone();
            PInvoke.DestroyIcon(shi.hIcon);
            return icon;
        }

        /// <summary>
        /// Gets the small icon for the specified shell item.
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconSmall(IntPtr pidl)
        {
            return GetIcon(pidl, PInvoke.SHGFI_SMALLICON);
        }

        /// <summary>
        /// Gets the large icon for the specified shell item.
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconLarge(IntPtr pidl)
        {
            return GetIcon(pidl, PInvoke.SHGFI_LARGEICON);
        }

        private static Icon GetIcon(IntPtr pidl, uint flags = 0)
        {
            PInvoke.SHFILEINFO shi = new PInvoke.SHFILEINFO();
            flags = flags | PInvoke.SHGFI_ICON | PInvoke.SHGFI_ADDOVERLAYS | PInvoke.SHGFI_PIDL;
            IntPtr hIcon = PInvoke.SHGetFileInfo(pidl, 0, out shi, (uint)(Marshal.SizeOf(shi)), flags);

            if (shi.hIcon == IntPtr.Zero) { return null; }

            Icon icon = (Icon)Icon.FromHandle(shi.hIcon).Clone();
            PInvoke.DestroyIcon(shi.hIcon);
            return icon;
        }

        /// <summary>
        /// Extracts the small icon from a resource file (such as imageres.dll).
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <param name="iconIndex">Index of the icon.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconSmall(string fileName, int iconIndex)
        {
            return GetIcon(fileName, iconIndex, false);
        }

        /// <summary>
        /// Extracts the large icon from a resource file (such as imageres.dll).
        /// </summary>
        /// <param name="fileName">Name of the file or folder.</param>
        /// <param name="iconIndex">Index of the icon.</param>
        /// <returns>
        /// The icon for the file or folder.
        /// </returns>
        public static Icon GetIconLarge(string fileName, int iconIndex)
        {
            return GetIcon(fileName, iconIndex, true);
        }

        private static Icon GetIcon(string fileName, int iconIndex, bool large)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { return null; }

            int iconCount = 0;
            IntPtr[] dummy = new IntPtr[1] { IntPtr.Zero };
            IntPtr[] icons = new IntPtr[1] { IntPtr.Zero };
            try
            {
                if (large)
                {
                    iconCount = PInvoke.ExtractIconEx(fileName, iconIndex, icons, dummy, 1);
                }
                else
                {
                    iconCount = PInvoke.ExtractIconEx(fileName, iconIndex, dummy, icons, 1);
                }

                Icon icon = null;
                if (iconCount > 0 && icons[0] != IntPtr.Zero)
                {
                    icon = (Icon)Icon.FromHandle(icons[0]).Clone(); 
                }
                return icon;
            }
            finally
            {
                ReleaseIcons(dummy);
                ReleaseIcons(icons);
            }

            void ReleaseIcons(IntPtr[] iconArray)
            {
                foreach (IntPtr ptr in iconArray)
                {
                    if (ptr != IntPtr.Zero)
                    {
                        PInvoke.DestroyIcon(ptr);
                    }
                }
            }
        }
    }
}
