/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorDalog.xaml.cs
 * PURPOSE:     Editor Window, Helps Generating Dialogs
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;

namespace EditorDialogTree
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The editor dialog class.
    /// </summary>
    public sealed partial class EditorDialog
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorDialogTree.EditorDialog" /> class.
        /// </summary>
        public EditorDialog()
        {
            InitializeComponent();
        }
    }
}