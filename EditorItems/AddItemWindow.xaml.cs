/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/AddItemWindow.cs
 * PURPOSE:     Add Basic Items to Database
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using System.Windows;
using System.Windows.Controls;
using AvalonRuntime;
using CommonControls;
using DatabaseDriver;

namespace EditorItems
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The add item window class.
    /// </summary>
    public sealed partial class AddItemWindow
    {
        /// <summary>
        ///     The armor Items.
        /// </summary>
        private ItemArmorEditor _armor;

        /// <summary>
        ///     The image Items.
        /// </summary>
        private ImageEditor _image;

        /// <summary>
        ///     The misc Items.
        /// </summary>
        private ItemMiscellaneousEditor _misc;

        /// <summary>
        ///     The weapon Items.
        /// </summary>
        private ItemWeaponEditor _weapon;

        /// <inheritdoc />
        /// <summary>
        ///     Entry point
        /// </summary>
        public AddItemWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Basic work we need to do after Loading
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeactivatedControls();
        }

        /// <summary>
        ///     Connect to Database
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            DeactivatedControls();

            var pathObj = FileIoHandler.HandleFileOpen(EditorItemsResources.DbExtension,
                Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder));

            if (pathObj == null) return;

            EditorItemsRegister.Path = pathObj.FilePath;

            var dbIn = HandlerInputSingleton.Create(pathObj.Folder, pathObj.FileNameWithoutExt);

            //Basic sanity check
            if (!dbIn.CheckDatabase()) return;

            TxtBoxPath.Text = pathObj.FilePath;

            CmbBxTable.IsEnabled = true;

            //Get basic data
            EditorItemsProcessing.SetIndex();
        }

        /// <summary>
        ///     Select the Item we want to work on
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Type</param>
        private void CmbBxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)CmbBxTable.SelectedItem;
            var name = item.Content.ToString();
            if (string.IsNullOrEmpty(name)) return;

            switch (name)
            {
                case EditorItemsResources.DbNameArmor:
                    HandleAmor();
                    break;

                case EditorItemsResources.DbNameMiscellaneous:
                    HandleMiscellaneous();
                    break;

                case EditorItemsResources.DbNameWeapon:
                    HandleWeapon();
                    break;

                case EditorItemsResources.DbNameImage:
                    HandleImage();
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        ///     Work on Weapons
        /// </summary>
        private void HandleWeapon()
        {
            CanvasBase.Children.Clear();
            _weapon = new ItemWeaponEditor();
            CanvasBase.Children.Add(_weapon);
        }

        /// <summary>
        ///     Work on Miscellaneous
        /// </summary>
        private void HandleMiscellaneous()
        {
            CanvasBase.Children.Clear();
            _misc = new ItemMiscellaneousEditor();
            CanvasBase.Children.Add(_misc);
        }

        /// <summary>
        ///     Work on Armor
        /// </summary>
        private void HandleAmor()
        {
            CanvasBase.Children.Clear();
            _armor = new ItemArmorEditor();
            CanvasBase.Children.Add(_armor);
        }

        /// <summary>
        ///     Work on Images
        /// </summary>
        private void HandleImage()
        {
            CanvasBase.Children.Clear();
            _image = new ImageEditor();
            CanvasBase.Children.Add(_image);
        }

        /// <summary>
        ///     Deactivate the Work Controls
        /// </summary>
        private void DeactivatedControls()
        {
            CmbBxTable.IsEnabled = false;
            CanvasBase.Children.Clear();
        }
    }
}