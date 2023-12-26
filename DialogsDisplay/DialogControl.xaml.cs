/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogControl.xaml.cs
 * PURPOSE:     User-control to Display the Core Dialog
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System;
using System.Windows;

namespace DialogsDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Display the Dialog
    /// </summary>
    public sealed partial class DialogControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DialogsDisplay.DialogControl" /> class.
        /// </summary>
        public DialogControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The dialog control user input.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void DialogControl_UserInput(object sender, DlgLine e)
        {
            UpdatedContents(e);
        }

        /// <summary>
        ///     Add Content to the Dialog
        /// </summary>
        /// <param name="e">The e.</param>
        private void UpdatedContents(DlgLine e)
        {
            if (string.IsNullOrEmpty(e.Line)) return;

            TxtBlkDialog.AppendText(string.Concat(e.Line, Environment.NewLine));
        }

        /// <summary>
        ///     The dialog control on loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void DialogControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            DialogsDisplayProcessing.DialogText += DialogControl_UserInput;
        }
    }
}