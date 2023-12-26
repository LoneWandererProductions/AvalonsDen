/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorTileDctEditor.xaml.cs
 * PURPOSE:     Helper Window to edit Tile Dictionary
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * Sources:     https://social.msdn.microsoft.com/Forums/vstudio/en-US/1776471a-c9e5-4a13-8970-b3687afbd961/creating-a-sideways-tab-control?forum=wpf
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CommonControls;
using Loader;
using Resources;

//TODO Delete Button, optional add Button

namespace Editors
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Editor to add Tiles
    /// </summary>
    internal sealed partial class EditorTileDctEditor
    {
        /// <summary>
        ///     Help Dictionary for creating the Tile Border Dictionary
        /// </summary>
        private Dictionary<int, TileBorders> _myBordersDct;

        /// <summary>
        ///     Help Dictionary for creating the Tile Dictionary
        /// </summary>
        private Dictionary<int, Tile> _tileDct;

        /// <inheritdoc />
        /// <summary>
        ///     Opens Existing TileBorder Dictionary for editing
        /// </summary>
        public EditorTileDctEditor()
        {
            InitiateValues();
            InitializeComponent();
        }

        /// <summary>
        ///     Data-binding for Border List
        /// </summary>
        public ObservableCollection<TileBorders> BorderLstCollection { get; set; }

        /// <summary>
        ///     Data-binding for Tile List
        /// </summary>
        public ObservableCollection<Tile> TileLstCollection { get; set; }

        /// <summary>
        ///     Initiate all Values
        /// </summary>
        private void InitiateValues()
        {
            TileLstCollection = EditorRegister.TileDct != null
                ? new ObservableCollection<Tile>(EditorRegister.GetTileDictionary())
                : new ObservableCollection<Tile>();

            BorderLstCollection = EditorRegister.BorderDct != null
                ? new ObservableCollection<TileBorders>(EditorRegister.GetBorderDictionary())
                : new ObservableCollection<TileBorders>();
        }

        /// <summary>
        ///     Open the border click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OpenBorder_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorResources.XmlFiles,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CoreFiles));

            if (pathObj != null) EditorRegister.SetBorderDictionary(WorkLoader.LoadTileBordersDct(pathObj.FilePath));

            BorderLstCollection = new ObservableCollection<TileBorders>(EditorRegister.GetBorderDictionary());
        }

        /// <summary>
        ///     Open the tile click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OpenTile_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorResources.XmlFiles,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CoreFiles));

            if (pathObj != null) EditorRegister.SetTileDictionary(WorkLoader.LoadTileDct(pathObj.FilePath));

            TileLstCollection = new ObservableCollection<Tile>(EditorRegister.GetTileDictionary());
        }

        /// <summary>
        ///     Save BorderDictionary
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BorderDctSaveAs_Click(object sender, RoutedEventArgs e)
        {
            _myBordersDct = new Dictionary<int, TileBorders>();

            //Conversion in Object Format
            for (var i = 0; i < BorderLstCollection.Count; i++) _myBordersDct.Add(i, BorderLstCollection[i]);

            EditorRegister.SetBorderDictionary(_myBordersDct);

            //Dialog
            var pathObj = FileIoHandler.HandleFileSave(EditorResources.XmlFiles,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CoreFiles));

            if (pathObj == null) return;

            EditorRegister.SaveBorders(pathObj.FilePath);
        }

        /// <summary>
        ///     Save the Results
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void TileDctSaveAs_Click(object sender, RoutedEventArgs e)
        {
            _tileDct = new Dictionary<int, Tile>();

            //Conversion in Object Format
            for (var i = 0; i < TileLstCollection.Count; i++) _tileDct.Add(i, TileLstCollection[i]);

            EditorRegister.SetTileDictionary(_tileDct);

            //Dialog
            var pathObj = FileIoHandler.HandleFileSave(EditorResources.XmlFiles,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CoreFiles));
            //path
            if (pathObj == null) return;
            //else
            EditorRegister.SaveTiles(pathObj.FilePath);
        }

        /// <summary>
        ///     The data grid loading row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The data grid row event arguments.</param>
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Close Window
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}