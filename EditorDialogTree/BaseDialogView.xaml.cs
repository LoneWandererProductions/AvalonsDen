/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/ChoiceDialogView.xaml.cs
 * PURPOSE:     Details for the Visual leaf, Base Dialog Part
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;

namespace EditorDialogTree
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Just show the Base Dialog
    /// </summary>
    internal sealed partial class BaseDialogView
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorDialogTree.BaseDialogView" /> class.
        /// </summary>
        public BaseDialogView()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The user control loaded.
        ///     Set Data Context
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Register.Cursor.BaseDialog;
        }
    }
}