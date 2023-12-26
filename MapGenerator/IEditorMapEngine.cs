/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/MapGenerator/IEditorMapEngine.cs
 * PURPOSE:     Map Generator Interface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;
using Resources;

namespace MapGenerator
{
    /// <summary>
    ///     just the basic Interface
    /// </summary>
    internal interface IEditorMapEngine
    {
        /// <summary>
        ///     Generates a complete Dummy Map
        /// </summary>
        /// <param name="mapHeight">Height of Map</param>
        /// <param name="mapLength">Length of Map</param>
        /// <returns>The MapObject as <see cref="MapObject" /> only a Dummy.</returns>
        MapObject Generate(int mapHeight, int mapLength);

        /// <summary>
        ///     Writes a BorderMap
        /// </summary>
        /// <param name="tileChange">Changed Tiles as Coordinates</param>
        /// <param name="map">Map Object</param>
        /// <param name="borderDct">MasterList of Borders</param>
        /// <param name="masterTileDct">MasterList of Tiles</param>
        /// <returns>MapObject as <see cref="MapObject" />.</returns>
        MapObject ChangeMap(List<Coordinates> tileChange, MapObject map, Dictionary<int, TileBorders> borderDct,
            Dictionary<int, Tile> masterTileDct);
    }
}