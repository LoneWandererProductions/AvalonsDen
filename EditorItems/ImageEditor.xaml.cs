/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/ItemArmorEditor.cs
 * PURPOSE:     UsercontrolAdd Basic Items to Database, Handle Armor
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * SOURCES:     http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
 */

using System;
using System.Windows;
using System.Windows.Controls;
using DatabaseDriver;
using Resources;

namespace EditorItems
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The image editor class.
    /// </summary>
    internal sealed partial class ImageEditor
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private string _id;

        /// <inheritdoc />
        /// <summary>
        ///     Entry Point
        /// </summary>
        internal ImageEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Load Data Grid
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetTable();
            EditorItemsProcessing.RefreshTable += Refresh;
        }

        /// <summary>
        ///     Switch to Selected Data Item
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
        private void TableData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableData.SelectedItem is not DbIndex tbi)
            {
                _id = string.Empty;
                return;
            }

            _id = tbi.Id;

            var dbOutput = HandlerOutputSingleton.CreateInstance(EditorItemsRegister.Path);

            DataContext = dbOutput.GetItemImage(_id);
        }

        /// <summary>
        ///     The Button click add Image.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Add(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.AddImage();
        }

        /// <summary>
        ///     The Button click save Image.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.SaveImages((Images)DataContext);
        }

        /// <summary>
        ///     The Button click delete Image.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_id)) EditorItemsProcessing.DeleteImages(_id);
        }

        /// <summary>
        ///     Refresh Overview Table
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Refresh(object sender, EventArgs e)
        {
            SetTable();
        }

        /// <summary>
        ///     Set Basic Overview Table
        /// </summary>
        private void SetTable()
        {
            if (DataContext != null) DataContext = null;

            if (TableData.ItemsSource != null) TableData.ItemsSource = null;

            var dbIn = HandlerInputSingleton.Instance;
            TableData.ItemsSource = dbIn.GetImageInfoTable();
        }
    }
}