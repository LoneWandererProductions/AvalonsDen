/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Chapter/Worlds.xaml.cs
 * PURPOSE:     Start Menu
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Debugger;

namespace Chapter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Start Class that controls it all
    /// </summary>
    internal sealed partial class Chapters
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="Chapters" /> class.
        /// </summary>
        public Chapters()
        {
            Switcher.Chapters = this;
            Switcher.Switch(new Index());
            InitializeComponent();
            DebugLog.CreateLogFile(ChapterResource.InformationLoaded, ErCode.Information);
        }

        /// <summary>
        ///     The navigate Control.
        /// </summary>
        /// <param name="nextPage">The nextPage.</param>
        internal void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }

        /// <summary>
        ///     The window closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            App.Log.StopDebugging();
            App.Log = null;
        }
    }
}