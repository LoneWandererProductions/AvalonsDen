/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorImageName.xaml.cs
 * PURPOSE:     Helps to set Background Image
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using System.Windows;
using CommonControls;

namespace Editors
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Basic Window for Background Image
    ///     Child to Editor
    /// </summary>
    internal sealed partial class EditorImageName
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Editors.EditorImageName" /> class.
        /// </summary>
        public EditorImageName()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Closes the Window
        ///     Notifies other window about the change
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Object Type</param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Saves Image Data
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Type</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            EditorRegister.MapObjct.BackGroundImage = ImageNameTextBox.Text;
        }

        /// <summary>
        ///     Searches File for Background Image
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Type</param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorResources.PngFiles,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CorePath));
            //Empty Return Value
            if (pathObj == null) return;

            ImageNameTextBox.Text = pathObj.FilePath;
        }

        /// <summary>
        ///     The button Information click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EditorStringResource.MessageBoxImageEditor, EditorStringResource.MessageBoxCaption,
                MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}