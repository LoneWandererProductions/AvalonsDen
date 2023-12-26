/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/MapGenerator/EditorMapEngine.cs
 * PURPOSE:     Implementation of Interface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using AvalonRuntime;
using ExtendedSystemObjects;
using Resources;

namespace MapGenerator
{
    /// <inheritdoc />
    /// <summary>
    ///     The editor map engine class.
    ///     TODO Improvement add the other directions
    /// </summary>
    public sealed class EditorMapEngine : IEditorMapEngine
    {
        /// <inheritdoc />
        /// <summary>
        ///     Writes a BorderMap
        /// </summary>
        /// <param name="tileChange">Changed Tiles as Coordinates</param>
        /// <param name="map">Map Object</param>
        /// <param name="borderDct">MasterList of Borders</param>
        /// <param name="masterTileDct">MasterList of Tiles</param>
        /// <returns>MapObject as <see cref="T:Resources.MapObject" />.</returns>
        public MapObject ChangeMap(List<Coordinates> tileChange, MapObject map,
            Dictionary<int, TileBorders> borderDct, Dictionary<int, Tile> masterTileDct)
        {
            //no changes return 0
            if (tileChange.IsNullOrEmpty()) return map;

            //Create a new Dummy for the Border just in case Border
            var borderArray = EditorMapEngineDummy.GenerateDummyBorderAsArray(map.Height, map.Length);

            tileChange = tileChange.OrderBy(tile => tile.ZLayer).ToList();

            borderArray = EditorMapEngineProcessing.ChangeBorderMap(borderArray, tileChange, masterTileDct,
                borderDct, map.Height, map.Length);

            //Generate Borders
            map.Borders = EditorMapEngineProcessing.GenerateBorderMap(map.Height, map.Length, borderArray);

            // Generate Map
            map.MapList = ConvertCoordinateToKeyValue(tileChange, map.Length);

            return map;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Generates a complete Dummy Map
        /// </summary>
        /// <param name="mapHeight">Height of Map</param>
        /// <param name="mapLength">Length of Map</param>
        /// <returns>The MapObject as <see cref="T:Resources.MapObject" /> only a Dummy.</returns>
        public MapObject Generate(int mapHeight, int mapLength)
        {
            var map = new MapObject(mapHeight, mapLength)
            {
                Borders = EditorMapEngineDummy.GenerateDummyBorder(mapHeight, mapLength)
            };
            return map;
        }

        /// <summary>
        ///     Generates a new MapObject Tile List from a list of changed Tiles
        /// </summary>
        /// <param name="tilechanges">Changed Tiles as Coordinates</param>
        /// <param name="length">Length of Map</param>
        /// <returns>Tile List for the MapObject</returns>
        private static List<SerializeableKeyValuePair.KeyValuePair<int, int>> ConvertCoordinateToKeyValue(
            List<Coordinates> tilechanges, int length)
        {
            return tilechanges.IsNullOrEmpty()
                ? null
                : (from tile in tilechanges
                    let masterid = ArtShared.CalculateId(tile, length)
                    select
                        new SerializeableKeyValuePair.KeyValuePair<int, int>(masterid, tile.TileId))
                .ToList();
        }
    }
}