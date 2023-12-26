/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorMap/MapEditor.cs
 * PURPOSE:     Helper Window to create a new Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;

namespace EditorMap
{
    /// <inheritdoc />
    /// <summary>
    ///     The Map Editor class.
    /// </summary>
    public sealed class MapEditor : IMapEditor
    {
        /// <inheritdoc />
        /// <summary>
        ///     Create a complete new Map
        /// </summary>
        public void CreateMap()
        {
            var mapEditor = new EditorMaps();
            mapEditor.ShowDialog();
            var mData = mapEditor.GetMapData();

            if (mapEditor.ShowMap && mData != null) MapData?.Invoke(null, mData);
        }

        /// <summary>
        ///     Public Event for refreshing/filling the Dialog Window
        /// </summary>
        public static event EventHandler<EventArgsMap> MapData;
    }
}