/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        EditorView/EditorMaps.xaml.cs
 * PURPOSE:     Helper Window to create a new Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Windows;
using Resources;

namespace EditorView
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Generates BorderMap and Tile Map
    ///     Starts them up with Dummies
    /// </summary>
    internal sealed partial class EditorMaps
    {
        /// <summary>
        ///     The map data.
        /// </summary>
        private EventArgsMap _mapData;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorView.EditorMaps" /> class.
        /// </summary>
        internal EditorMaps()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets a value indicating whether we load the Map or not
        /// </summary>
        internal bool ShowMap { get; private set; }

        /// <summary>
        ///     Get everything in Place
        /// </summary>
        /// <param name="sender">Control Sender</param>
        /// <param name="e">Type of Event</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mapData = new EventArgsMap
            {
                Map = new List<SerializeableKeyValuePair.KeyValuePair<int, int>>(),
                Transitions = new Dictionary<int, List<int>>()
            };
            DataContext = _mapData;
        }

        /// <summary>
        ///     Create a new Map with the called parameters
        ///     Or get the Size for resizing a new Map
        /// </summary>
        /// <param name="sender">Control Sender</param>
        /// <param name="e">Type of Event</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ShowMap = true;
            Close();
        }

        /// <summary>
        ///     Close Window
        /// </summary>
        /// <param name="sender">Control Sender</param>
        /// <param name="e">Type of Event</param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ShowMap = false;
            Close();
        }

        /// <summary>
        ///     Return the new created Map
        /// </summary>
        /// <returns>New Mag Infos</returns>
        internal EventArgsMap GetMapData()
        {
            return _mapData;
        }
    }
}