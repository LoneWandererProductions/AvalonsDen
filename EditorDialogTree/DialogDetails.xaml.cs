/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/DialogDetails.xaml.cs
 * PURPOSE:     Helper Window for Editing Dialog
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.ComponentModel;
using System.Windows;

namespace EditorDialogTree
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaction logic for DialogDetails.xaml
    /// </summary>
    internal sealed partial class DialogDetails
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorDialogTree.DialogDetails" /> class.
        /// </summary>
        internal DialogDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Public Event for refreshing/filling the Dialog Window
        /// </summary>
        public static event EventHandler DialogChange;

        /// <summary>
        ///     We tell the main Window, that it should probably redraw
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            DialogChange?.Invoke(null, EventArgs.Empty);
        }
    }
}