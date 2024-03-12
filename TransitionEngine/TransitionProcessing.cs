/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/TransitionProcessing.cs
 * PURPOSE:     Here we handle the heavy lifting
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExtendedSystemObjects;
using Resources;

// ReSharper disable ArrangeBraces_ifelse
// ReSharper disable ArrangeBraces_foreach
// ReSharper disable LoopCanBeConvertedToQuery, for once

namespace TransitionEngine
{
    /// <summary>
    ///     The transition processing class.
    /// </summary>
    internal static class TransitionProcessing
    {
        /// <summary>
        ///     Check if a Multi Terrain Tile is in the list
        /// </summary>
        /// <param name="tileId">Id of the Tile</param>
        /// <returns>Id witch is part of the Multi Terrain File</returns>
        internal static bool CheckIfTransitionTerrain(int tileId)
        {
            var tile = TransitionEngineRegister.GetTile(tileId);
            return tile?.TileType == Tile.TileTypes.MultiTerrain;
        }

        /// <summary>
        ///     We get the Master Id of the Transition
        /// </summary>
        /// <param name="masterId">Id of the Transition</param>
        /// <returns>Id of the Master or -1 if nothing was found</returns>
        internal static int GetMasterTile(int masterId)
        {
            var tile = TransitionEngineRegister.GetTile(masterId);
            if (tile == null)
            {
                return TransitionEngineResources.ErrorNumber;
            }

            masterId = tile.IdOfMaster;

            for (var i = masterId; i >= 0; i--)
            {
                tile = TransitionEngineRegister.GetTile(i);
                if (tile == null)
                {
                    return TransitionEngineResources.ErrorNumber;
                }

                var id = tile.IdOfMaster;
                if (i == id)
                {
                    return id;
                }
            }

            return TransitionEngineResources.ErrorNumber;
        }

        /// <summary>
        ///     Get all the locations that use these Tile
        /// </summary>
        /// <param name="masterTileId">Id of the Multi Terrain Tile</param>
        /// <param name="transitionDct">All Tiles on the Map</param>
        /// <returns>List of Locations that have these Tile in USe</returns>
        internal static IEnumerable<int> GetAllStartPoints(int masterTileId,
            Dictionary<int, List<int>> transitionDct)
        {
            return (from tile in transitionDct
                from entry in tile.Value
                where entry == masterTileId
                select tile.Key).ToList();
        }

        /// <summary>
        ///     Checks if tile is contained
        /// </summary>
        /// <param name="multiTileId">Id of the Tile</param>
        /// <param name="multiTile">Multi Terrain Tile</param>
        /// <returns>Checks if the Id is contained</returns>
        internal static bool DoesMultiTileContainTile(int multiTileId, Dictionary<int, int> multiTile)
        {
            return multiTile.Keys.Any(tile => tile == multiTileId);
        }

        /// <summary>
        ///     Checks if a Multi terrain is already in Place
        /// </summary>
        /// <param name="multiTile">Coordinate Id, Tile Id</param>
        /// <param name="transitionDct">Coordinate Id, List of Tile Id</param>
        /// <returns>If we have something in it we return false</returns>
        internal static bool CheckMultiTerrainPlacement(Dictionary<int, int> multiTile,
            Dictionary<int, List<int>> transitionDct)
        {
            foreach (var tile in multiTile)
            {
                if (!transitionDct.ContainsKey(tile.Key))
                {
                    continue;
                }

                var transition = transitionDct[tile.Key];

                if (
                    transition.Any(
                        id =>
                            TransitionEngineRegister
                                .GetTile(id)
                                .TileType == Tile.TileTypes.MultiTerrain
                            && id != tile.Value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Generated huge Tile with a linked List one successor one predecessor
        ///     A Multi Terrain Tile
        ///     Master Tile is included
        /// </summary>
        /// <param name="tilePlacement">Direction/Tile</param>
        /// <param name="adjCoordinate">All Transitions associated with the Master Tile</param>
        /// <param name="masterId">Id of Coordinate</param>
        /// <param name="tileId">Id of the Tile</param>
        /// <returns>The Multi Tile and his Placement on the Map</returns>
        internal static Dictionary<int, int> GetComplexTransitions(Dictionary<int, int> tilePlacement,
            AdjacentCoordinate adjCoordinate, int masterId, int tileId)
        {
            //New Idea:
            //Collect All Transitions for the specific huge tile than Calculate tile-placement
            //run though every tile-placement with for each call it with Dictionary
            //no Recursion

            if (tilePlacement.IsNullOrEmpty())
            {
                return null;
            }

            //                                  Place    ,   Tile
            var dct = new Dictionary<int, int> { { masterId, tileId } };

            tilePlacement.Remove(0);

            foreach (var tile in tilePlacement)
            {
                // Tile / Direction
                var dict = new Dictionary<int, int> { { tile.Key, tile.Value } };

                Debug.WriteLine(TransitionEngineResources.KeyName + tile.Key);

                var cache = adjCoordinate.GenerateCoordinates(dict, masterId);

                //No adjCoordinate Chain is broken, if it is more than one, the successor was wrong
                if (cache.IsNullOrEmpty() || cache.Count > 1)
                {
                    return null;
                }

                masterId = cache.First().Key;

                dct.Add(masterId, tile.Key);
            }

            return dct;
        }

        /// <summary>
        ///     Generated huge Tile with a linked List one successor one predecessor
        ///     A Multi Terrain Tile
        ///     Master Tile is included
        /// </summary>
        /// <param name="tileId">Id of the Tile</param>
        /// <returns>Tiles and Directions</returns>
        internal static Dictionary<int, int> GetAllMultiTransitions(int tileId)
        {
            //                                              Tile Id,   Direction
            var tilePlacement = new Dictionary<int, int> { { tileId, TransitionEngineResources.CoordinateDirection } };

            while (true)
            {
                var tile = tilePlacement.Last();

                var placement = TransitionEngineRegister.GetAllTransitions(tile.Key);

                //break out since we are finished here
                if (placement.IsNullOrEmpty())
                {
                    return tilePlacement;
                }

                var place = placement.First();

                //catch Error
                if (tilePlacement.ContainsKey(place.Key))
                {
                    return null;
                }

                //                  Tile Id , direction
                tilePlacement.Add(place.Key, place.Value);
            }
        }

        /// <summary>
        ///     Id of Coordinate and Id of Tile
        ///     Furthermore a List with all Child Transitions in a Dictionary the Id defines the Coordinate
        ///     Id 0, Child's 1,2,3,4, ....
        ///     .
        ///     .
        ///     .
        ///     Adds the changes to the list
        /// </summary>
        /// <param name="tileDct">Coordinate as int, Tiles as Int, collected in a list</param>
        /// <param name="tilePlacement">Coordinate as int, Tile Number as int</param>
        /// <param name="isMultiTile">Is it a Multi Tile?, If yes add it first, else just append</param>
        /// <returns>Transition Dictionary with added Elements</returns>
        internal static Dictionary<int, List<int>> ConvergeParts(Dictionary<int, List<int>> tileDct,
            Dictionary<int, int> tilePlacement, bool isMultiTile)
        {
            //return complex object with new Tiles and Removed Tiles, basic coordinate is needed not the values itself
            if (tilePlacement.IsNullOrEmpty())
            {
                return tileDct;
            }

            //Initiate Changed Tile List

            foreach (var tile in tilePlacement)
            {
                // if we use this key already we do some basic logic checks
                if (tileDct.ContainsKey(tile.Key))
                {
                    var list = tileDct[tile.Key];
                    if (list.Contains(tile.Value))
                    {
                        continue;
                    }

                    //if Multi Terrain add it at the beginning, we add the results to the list, later we might remove them, not optimal but it should work!
                    if (isMultiTile)
                    {
                        list.AddFirst(tile.Value);
                    }
                    // else append it
                    else
                    {
                        list.Add(tile.Value);
                    }

                    //Check if we overlay Master Tile(Key) with an Transition of Itself
                    //Complex Song and Dance, remove all Transitions of the existing master Multi Terrain Tiles with Transitions
                    //First get all Present Master Ids in the Selection
                    var ids = list.Select(i => i).Intersect(TransitionEngineRegister.MasterId).ToList();

                    //if we have some get all Transitions
                    if (!ids.IsNullOrEmpty())
                    {
                        //Get Transitions by master on the field and remove their Child Transitions
                        foreach (var id in ids)
                        {
                            var variations = TransitionEngineRegister.GetAllTransitions(id).Keys.ToList();
                            //create a reduced list
                            list = list.Except(variations).ToList();
                        }
                    }

                    tileDct[tile.Key] = list;
                }
                //nothing fancy just add the transitions to the dictionary
                else
                {
                    var tileLst = new List<int> { tile.Value };
                    tileDct.Add(tile.Key, tileLst);
                }
            }

            return tileDct;
        }

        /// <summary>
        ///     Delete Transitions from the Master List
        /// </summary>
        /// <param name="tileDct">Coordinate as int, Tiles as Int, collected in a list</param>
        /// <param name="tilePlacement">Coordinate as int, Tile Number as Int</param>
        /// <returns>Removed Elements</returns>
        internal static Dictionary<int, List<int>> RemoveParts(Dictionary<int, List<int>> tileDct,
            Dictionary<int, int> tilePlacement)
        {
            foreach (var tile in tilePlacement.Where(tile => tileDct.ContainsKey(tile.Key)))
            {
                //add to the changed Tile List
                var list = tileDct[tile.Key];
                //Remove Element
                if (list.Contains(tile.Value))
                {
                    list.Remove(tile.Value);
                }

                //Remove whole Entry
                if (list.IsNullOrEmpty())
                {
                    tileDct.Remove(tile.Key);
                }
            }

            return tileDct;
        }
    }
}