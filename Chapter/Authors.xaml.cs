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
    ///     Simple Class for credits
    /// </summary>
    internal sealed partial class Authors
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Chapter.Authors" /> class.
        /// </summary>
        internal Authors()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     On load-up fill in all the content.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Directory.GetCurrentDirectory();

            //check if File exists
            if (!File.Exists(path + ChapterResource.AuthorsFile)) return;

            foreach (var element in ReadText.ReadFile(string.Concat(path, ChapterResource.AuthorsFile)))
                TextBoxAuthors.AppendText(string.Concat(element, Environment.NewLine));
        }

        /// <summary>
        ///     Go back to Index
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Index());
        }
    }
}