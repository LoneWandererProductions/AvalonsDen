/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/ItemWeaponEditor.cs
 * PURPOSE:     UsercontrolAdd Basic Items to Database, Handle Weapon
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
    ///     The item weapon editor class.
    /// </summary>
    internal sealed partial class ItemWeaponEditor
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private string _id;

        /// <inheritdoc />
        /// <summary>
        ///     Entry Point
        /// </summary>
        internal ItemWeaponEditor()
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
            //could be done via WPF but well Fuck me and WPF for that matter
            CbxSlot.ItemsSource = Enum.GetValues(typeof(InventoryEnum.EnumSlot)).Cast<InventoryEnum.EnumSlot>();
            CbxType.ItemsSource = Enum.GetValues(typeof(GearWeapon.DamageType)).Cast<GearWeapon.DamageType>();
            CbxRange.ItemsSource = Enum.GetValues(typeof(GearWeapon.Range)).Cast<GearWeapon.Range>();
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
            DataContext = dbOutput.GetItemWeapon(tbi.Id);
        }

        /// <summary>
        ///     The Button click add Weapon.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Add(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.AddWeapon();
        }

        /// <summary>
        ///     The Button click save Weapon.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            EditorItemsProcessing.SaveWeapon((Weapon) DataContext);
        }

        /// <summary>
        ///     The Button click delete Weapon.
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
            TableData.ItemsSource = dbIn.GetWeaponInfoTable();
        }
    }
}