/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/ItemMiscellaneousEditor.cs
 * PURPOSE:     UserControl Adds Basic Items to Database, Handle Miscellaneous
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DatabaseDriver;
using Resources;

namespace EditorItems
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The item miscellaneous editor class.
    /// </summary>
    internal sealed partial class ItemMiscellaneousEditor
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private string _id;

        /// <inheritdoc />
        /// <summary>
        ///     Entry Point
        /// </summary>
        internal ItemMiscellaneousEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Load Data Grid
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The selection changed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetTable();
            EditorItemsProcessing.RefreshTable += Refresh;
            //could be done via WPF but well Fuck me and WPF for that matter
            CbxType.ItemsSource = Enum.GetValues(typeof(GearMisc.Type)).Cast<GearMisc.Type>();
            CxbSlot.ItemsSource = Enum.GetValues(typeof(InventoryEnum.EnumSlot)).Cast<InventoryEnum.EnumSlot>();
        }

        /// <summary>
        ///     Switch to Selected Data Item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The selection changed event arguments.</param>
        private void TableData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableData.SelectedItem is not DbIndex tbi)
            {
                _id = string.Empty;
                return;
            }

            _id = tbi.Id;

            var dbOutput = HandlerOutputSingleton.CreateInstance(EditorItemsRegister.Path);
            DataContext = dbOutput.GetItemMiscellaneous(tbi.Id);
        }

        /// <summary>
        ///     The Button click add Item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The selection changed event arguments.</param>
        private void Btn_Click_Add(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.AddMiscellaneous();
        }

        /// <summary>
        ///     The Button click save Item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.SaveMiscellaneous((Miscellaneous) DataContext);
        }

        /// <summary>
        ///     The Button click delete Item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_id)) EditorItemsProcessing.DeleteItem(_id);
        }

        /// <summary>
        ///     Refresh Overview Table
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
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
            TableData.ItemsSource = dbIn.GetMiscellaneousInfoTable();
        }
    }
}