// -----------------------------------------------------------------------
// <copyright file="IconExtensions.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Extension methods for Icon class.
    /// </summary>
    public static class IconExtensions
    {
        /// <summary>
        /// Extension method for <seealso cref="Icon"/> class to convert
        /// reference to an icon into a WPF <seealso cref="ImageSource"/>.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public static ImageSource ToImageSource(this Icon icon)
        {
            if (icon == null) { return null; }

            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }

            return wpfBitmap;
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}
