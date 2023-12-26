/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorToolBox.Xaml.cs
 * PURPOSE:     Window to Display Tiles
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using CommonControls;
using ExtendedSystemObjects;
using Mathematics;
using Renderer;
using Resources;

namespace Editors
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     ToolBox for Events and Tiles
    /// </summary>
    internal sealed partial class EditorTileToolBox
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initialize Component
        /// </summary>
        /// <param name="tileDct">Dictionary of all Tiles</param>
        public EditorTileToolBox(Dictionary<int, Tile> tileDct)
        {
            TileDct = tileDct;
            InitializeComponent();
        }

        /// <summary>
        ///     Gets the tile Dictionary.
        /// </summary>
        /// <value>
        ///     The tile Dictionary.
        /// </value>
        private Dictionary<int, Tile> TileDct { get; }

        /// <summary>
        ///     Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (TileDct.IsNullOrEmpty()) return;

            One.ThumbCellSize = Two.ThumbCellSize = Three.ThumbCellSize = 100;

            //Initialize
            var tileOne = new Dictionary<int, string>();
            var tileTwo = new Dictionary<int, string>();
            var tileThree = new Dictionary<int, string>();

            foreach (var (key, value) in TileDct)
                switch (value.TileType)
                {
                    case Tile.TileTypes.NoTransitions:
                        tileOne.Add(key, Path.Combine(EditorResources.TilesFolder, value.FileName));
                        break;

                    case Tile.TileTypes.MultiTerrain:
                        tileTwo.Add(key, Path.Combine(EditorResources.TilesFolder, value.FileName));
                        break;

                    case Tile.TileTypes.TerrainWithTransitions:
                        tileThree.Add(key, Path.Combine(EditorResources.TilesFolder, value.FileName));
                        break;
                }

            var fract = new ExtendedMath.Fraction(tileOne.Count, 2);
            var count = fract.Exponent + fract.Numerator;

            if (count == 1)
                One.ThumbHeight = 1;
            else
                One.ThumbHeight = fract.Exponent + fract.Numerator;
            One.ThumbLength = 2;
            One.ItemsSource = tileOne;

            fract = new ExtendedMath.Fraction(tileTwo.Count, 2);
            count = fract.Exponent + fract.Numerator;

            if (count == 1)
                Two.ThumbHeight = 1;
            else
                Two.ThumbHeight = fract.Exponent + fract.Numerator;
            Two.ThumbLength = 2;
            Two.ItemsSource = tileTwo;

            fract = new ExtendedMath.Fraction(tileThree.Count, 2);
            count = fract.Exponent + fract.Numerator;

            if (count == 1)
                Three.ThumbHeight = 1;
            else
                Three.ThumbHeight = fract.Exponent + fract.Numerator;
            Three.ThumbLength = 2;
            Three.ItemsSource = tileThree;
        }

        /// <summary>
        ///     The item selected item event of the <see cref="EventHandler{TEventArgs}" />.
        /// </summary>
        public static event EventHandler<BoxClickedEventArgs> ItemSelectedItem;

        /// <summary>
        ///     Delete Tile Button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void TileDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new BoxClickedEventArgs
            {
                TileId = EditorResources.IdOfDeletePassableTile
            };
            OnItemSelectedItem(args);
        }

        /// <summary>
        ///     Delete Event Button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void EventDeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new BoxClickedEventArgs
            {
                TileId = EditorResources.IdOfDeleteEvent
            };
            OnItemSelectedItem(args);
        }

        /// <summary>
        ///     Add Event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void EventBoxButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new BoxClickedEventArgs
            {
                TileId = EditorResources.IdOfEvent
            };
            OnItemSelectedItem(args);
        }

        /// <summary>
        ///     Delete Multi Terrain
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void DelMultiEventButton_Click(object sender, RoutedEventArgs e)
        {
            var args = new BoxClickedEventArgs
            {
                TileId = EditorResources.IdOfMultiTerrain
            };
            OnItemSelectedItem(args);
        }

        /// <summary>
        ///     Trigger Event
        ///     Notify Subscribers
        /// </summary>
        /// <param name="args">The box clicked event arguments.</param>
        private void OnItemSelectedItem(BoxClickedEventArgs args)
        {
            ItemSelectedItem?.Invoke(null, args);
        }

        /// <summary>
        ///     Raises the <see cref="E:ImageClicked" /> event.
        /// </summary>
        /// <param name="itemId">The <see cref="ImageEventArgs" /> instance containing the event data.</param>
        private void ImageClicked(ImageEventArgs itemId)
        {
            var args = new BoxClickedEventArgs
            {
                TileId = itemId.Id
            };
            OnItemSelectedItem(args);
        }
    }
}