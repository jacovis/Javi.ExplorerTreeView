// -----------------------------------------------------------------------
// <copyright file="CompareStringNatural.cs">
// All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Javi.ExplorerTreeView
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Compare two strings in natural order instead of ascii order. 
    /// </summary>
    /// <seealso cref="System.Collections.IComparer" />
    public class CompareStringNatural : IComparer
    {
        /// <summary>
        /// Compares two strings and returns a value indicating whether one is less than,
        /// equal to, or greater than the other.
        /// Uses a natural compare as explained here: https://stackoverflow.com/questions/248603/natural-sort-order-in-c-sharp.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of x and y.
        /// Value Meaning:
        /// Less than zero x is less than y.
        /// Zero x equals y.
        /// Greater than zero x is greater than y.
        /// </returns>
        /// <exception cref="System.ArgumentException">One or both arguments is not of type string.</exception>
        public Int32 Compare(object x, object y)
        {
            if (!(x is string) || !(y is string))
            {
                throw new ArgumentException("One or both arguments is not of type string.");
            }

            return string.Compare(Regex.Replace(x as string, @"\d+", s => s.Value.PadLeft(50, '0')),
                Regex.Replace(y as string, @"\d+", s => s.Value.PadLeft(50, '0')));
        }
    }

}
