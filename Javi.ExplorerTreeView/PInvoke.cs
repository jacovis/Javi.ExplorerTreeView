// -----------------------------------------------------------------------
// <copyright file="PInvoke.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Platform invoke helper class.
    /// </summary>
    public static class PInvoke
    {
        #region SHGetFileInfo
        public const uint SHGFI_ICON = 0x000000100;               // get icon
        public const uint SHGFI_ADDOVERLAYS = 0x000000020;        // for icons, apply the appropriate overlays
        public const uint SHGFI_DISPLAYNAME = 0x000000200;        // get display name
        public const uint SHGFI_TYPENAME = 0x000000400;           // get type name
        public const uint SHGFI_ATTRIBUTES = 0x000000800;         // get attributes
        public const uint SHGFI_ICONLOCATION = 0x000001000;       // get icon location
        public const uint SHGFI_EXETYPE = 0x000002000;            // return exe type
        public const uint SHGFI_SYSICONINDEX = 0x000004000;       // get system icon index
        public const uint SHGFI_LINKOVERLAY = 0x000008000;        // put a link overlay on icon
        public const uint SHGFI_SELECTED = 0x000010000;           // show icon in selected state
        public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
        public const uint SHGFI_LARGEICON = 0x000000000;          // get large icon
        public const uint SHGFI_SMALLICON = 0x000000001;          // get small icon
        public const uint SHGFI_OPENICON = 0x000000002;           // get open icon
        public const uint SHGFI_SHELLICONSIZE = 0x000000004;      // get shell size icon
        public const uint SHGFI_PIDL = 0x000000008;               // pszPath is a pidl
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;  // use passed dwFileAttribute

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbSizeFileInfo, uint flags);
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(IntPtr pidl, uint dwFileAttributes, out SHFILEINFO psfi, uint cbSizeFileInfo, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        #endregion

        #region SHGetPathFromIDListW
        /// <summary>
        /// Converts an item identifier list to a file system path. (Note: SHGetPathFromIDList calls the ANSI version, must call SHGetPathFromIDListW for .NET)
        /// </summary>
        /// <param name="pidl">Address of an item identifier list that specifies a file or directory location relative to the root of the namespace (the desktop).</param>
        /// <param name="pszPath">Address of a buffer to receive the file system path. This buffer must be at least MAX_PATH characters in size.</param>
        /// <returns>Returns TRUE if successful, or FALSE otherwise. </returns>
        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SHGetPathFromIDListW(IntPtr pidl, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszPath);
        #endregion

        #region SHParseDisplayName
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern uint SHParseDisplayName(string pszName, IntPtr zero, [Out] out IntPtr ppidl, uint sfgaoIn, [Out] out uint psfgaoOut);
        #endregion

        #region ExtractIconEx
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);
        #endregion

        #region DestroyIcon
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        #endregion
    }
}