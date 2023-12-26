/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/CellsProcessing.cs
 * PURPOSE:     Helper Class
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using AvalonRuntime;
using Debugger;
using ExtendedSystemObjects;
using Resources;

namespace Renderer
{
    /// <summary>
    ///     The cells processing class.
    /// </summary>
    internal static class CellsProcessing
    {
        /// <summary>
        ///     Used for normal Terrain Tiles
        /// </summary>
        /// <param name="mapDictionary">Format the Map is saved in</param>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <param name="width">Width of Map</param>
        /// <returns>Dictionary with Coordinates and Filenames</returns>
        internal static Dictionary<Coordinates, string> ConvertMap(
            List<SerializeableKeyValuePair.KeyValuePair<int, int>> mapDictionary, Dictionary<int, Tile> tileDct,
            int width)
        {
            //something went completely wrong
            if (mapDictionary.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningMapEmpty, ErCode.Warning);
                return new Dictionary<Coordinates, string>();
            }

            var coordinateIdlayer = new Dictionary<Coordinates, string>();

            foreach (var tile in mapDictionary)
            {
                if (!tileDct.ContainsKey(tile.Value)) continue;

                var fileName = tileDct[tile.Value].FileName;
                var layer = tileDct[tile.Value].Layer;

                //calculate our Coordinate
                coordinateIdlayer.Add(ArtShared.IdToCoordinate(tile.Key, width, layer), fileName);
            }

            return coordinateIdlayer;
        }

        /// <summary>
        ///     Used for Multi Terrain Tiles
        /// </summary>
        /// <param name="transitions">Transitions</param>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <param name="width">Width of Map</param>
        /// <returns>Dictionary with Coordinates and List of Filenames</returns>
        /// TODO Convert to int
        internal static Dictionary<Coordinates, List<int>> ConvertTransitions(
            Dictionary<int, List<int>> transitions, Dictionary<int, Tile> tileDct, int width)
        {
            if (transitions.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningTransitionsEmpty, ErCode.Warning);
                return new Dictionary<Coordinates, List<int>>();
            }

            //we must add the tile to the list at Least in Editor Mode
            var coordinateIdlayer = new Dictionary<Coordinates, List<int>>();

            foreach (var target in transitions)
            foreach (var tile in target.Value)
            {
                if (!tileDct.ContainsKey(tile)) continue;

                var layer = tileDct[tile].Layer;

                var point = ArtShared.IdToCoordinate(target.Key, width, layer);

                if (coordinateIdlayer.ContainsKey(point))
                {
                    var lst = coordinateIdlayer[point];
                    lst.Add(tile);
                    coordinateIdlayer[point] = lst;
                }
                else
                {
                    var lst = new List<int> { tile };
                    coordinateIdlayer.Add(point, lst);
                }
            }

            return coordinateIdlayer;
        }

        /// <summary>
        ///     Get the Max Layer of a Tile Dictionary
        /// </summary>
        /// <param name="tileDct">Tile Dictionary</param>
        /// <returns>Max layer we need to generate</returns>
        internal static int GetMaxLayer(Dictionary<int, Tile> tileDct)
        {
            return tileDct.Select(tile => tile.Value.Layer).Concat(new[] { 0 }).Max();
        }

        /// <summary>
        ///     Convert from Saved Files
        ///     Only used from Editor
        /// </summary>
        /// <param name="mapDictionary">Format the Map is saved in</param>
        /// <param name="width">Width of Map</param>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <returns>List of Coordinates</returns>
        internal static List<Coordinates> ConvertMapToList(
            IEnumerable<SerializeableKeyValuePair.KeyValuePair<int, int>> mapDictionary, int width,
            Dictionary<int, Tile> tileDct)
        {
            return (from tile in mapDictionary
                let layer = tileDct[tile.Value].Layer
                select ArtShared.IdToCoordinate(tile.Key, width, layer, tile.Value)).ToList();
        }
    }
}