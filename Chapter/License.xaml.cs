/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Chapter/License.cs
 * PURPOSE:     License Viewer
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.IO;
using System.Windows;
using DataFormatter;

namespace Chapter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The license class.
    /// </summary>
    internal sealed partial class License
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Chapter.License" /> class.
        /// </summary>
        internal License()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The user control loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Directory.GetCurrentDirectory();
            //check if File exists
            if (!File.Exists(Path.Combine(path, ChapterResource.LicenseFile))) return;

            foreach (var element in ReadText.ReadFile(string.Concat(path, ChapterResource.LicenseFile)))
                TextBoxLicense.AppendText(string.Concat(element, Environment.NewLine));
        }

        /// <summary>
        ///     Go back to Index.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void FlipPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Index());
        }
    }
}