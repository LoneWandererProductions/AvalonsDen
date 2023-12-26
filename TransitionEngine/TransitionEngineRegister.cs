/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/TransitionEngineRegister.cs
 * PURPOSE:     Hold basic values and handle some basic transformations for speed ups and better readability
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using ExtendedSystemObjects;
using Resources;

namespace TransitionEngine
{
    /// <summary>
    ///     The transition engine register class.
    /// </summary>
    internal static class TransitionEngineRegister
    {
        /// <summary>
        ///     Holds the Master List
        /// </summary>
        private static Dictionary<int, Tile> _tileDct;

        /// <summary>
        ///     optimization
        ///     Hold calculated values
        /// </summary>
        private static int _tileId;

        /// <summary>
        ///     optimization
        ///     Hold calculated values
        /// </summary>
        private static Dictionary<int, int> _transitions;

        /// <summary>
        ///     The Master Tile Dictionary.
        /// </summary>
        internal static Dictionary<int, Tile> TileDct
        {
            private get { return _tileDct; }
            set
            {
                _tileDct = value;
                GetMasterTile();
            }
        }

        /// <summary>
        ///     we will use this to check the master Ids
        /// </summary>
        internal static List<int> MasterId { get; private set; }

        /// <summary>
        ///     Get the Tile per Id
        /// </summary>
        /// <param name="tileId">Id of Tile</param>
        /// <returns>Returns null or the Tile</returns>
        internal static Tile GetTile(int tileId)
        {
            return TileDct?[tileId];
        }

        /// <summary>
        ///     Returns all Transitions for a specific Tile
        /// </summary>
        /// <param name="tileId">Id of the Tile</param>
        /// <returns>DirectionOfTransition, Tile Key, Value direction</returns>
        internal static Dictionary<int, int> GetAllTransitions(int tileId)
        {
            if (TileDct?.ContainsKey(tileId) != true) return null;

            //optimization
            if (_tileId == tileId && !_transitions.IsNullOrEmpty()) return _transitions;

            var transitions = TileDct.Where(
                    tile => tile.Value.IdOfMaster == tileId && tile.Key != tile.Value.IdOfMaster)
                .ToDictionary(tile => tile.Key, tile => tile.Value.DirectionOfTransition);

            //lets try to speed up some things shoot me but should actual work
            if (!transitions.IsNullOrEmpty()) return transitions;

            _tileId = tileId;
            _transitions = transitions;

            return transitions;
        }

        /// <summary>
        ///     Returns the first Tile Id which is an Multi Terrain Tile. Last in First out.
        /// </summary>
        /// <param name="transitions">Transitions of specified Coordinate we want to delete</param>
        /// <returns>Id of Tile or -1 if nothing was found</returns>
        internal static int CheckIfMultiTerrain(IEnumerable<int> transitions)
        {
            foreach (
                var tileId in
                transitions.Where(TileDct.ContainsKey)
                    .Where(tileId => GetTile(tileId).TileType == Tile.TileTypes.MultiTerrain)
            )
                return tileId;

            return TransitionEngineResources.ErrorNumber;
        }

        /// <summary>
        ///     Get the Masters of all Transitions Tiles
        /// </summary>
        private static void GetMasterTile()
        {
            MasterId = (from tile in TileDct
                where tile.Value.TileType == Tile.TileTypes.TerrainWithTransitions && tile.Value.IdOfMaster == 0
                select tile.Key).ToList();
        }
    }
}