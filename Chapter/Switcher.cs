/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Chapter/Switcher.cs
 * PURPOSE:     Start Menu
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows.Controls;

namespace Chapter
{
    /// <summary>
    ///     The switcher class.
    /// </summary>
    internal static class Switcher
    {
        /// <summary>
        ///     The chapters.
        /// </summary>
        internal static Chapters Chapters { get; set; }

        /// <summary>
        ///     Switch.
        /// </summary>
        /// <param name="newPage">The newPage.</param>
        internal static void Switch(UserControl newPage)
        {
            Chapters.Navigate(newPage);
        }
    }
}