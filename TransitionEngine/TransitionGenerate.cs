/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/Transition.cs
 * PURPOSE:     Create and Handle Transitions for a Map, simple Transitions and Multi Terrain
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using AvalonRuntime;
using Debugger;
using ExtendedSystemObjects;
using Resources;

// ReSharper disable LoopCanBeConvertedToQuery, for once

namespace TransitionEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     The transition generate class.
    /// </summary>
    public sealed class TransitionGenerate : ITransitionGenerate
    {
        /// <summary>
        ///     Initiate for a New Map
        ///     Use this one for a New Map
        /// </summary>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <param name="tileDct">Master Tile List</param>
        public TransitionGenerate(int height, int length, Dictionary<int, Tile> tileDct)
        {
            TransitionEngineRegister.TileDct = tileDct;
            Length = length;

            AdjCoordinate = new AdjacentCoordinate(height, length);
            TransitionDictionary = new Dictionary<int, List<int>>();
        }

        /// <summary>
        ///     Initiate for a existing Map
        ///     Use this one if you load a map
        /// </summary>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <param name="tileDct">The tile Dictionary.</param>
        /// <param name="transitionDictionary">Existing TransitionDictionary</param>
        public TransitionGenerate(int height, int length, Dictionary<int, Tile> tileDct,
            Dictionary<int, List<int>> transitionDictionary)
        {
            Length = length;
            TransitionEngineRegister.TileDct = tileDct;

            AdjCoordinate = new AdjacentCoordinate(height, length);
            //basic Security check
            transitionDictionary ??= new Dictionary<int, List<int>>();

            TransitionDictionary = transitionDictionary;
        }

        /// <summary>
        ///     The adjacent coordinate.
        /// </summary>
        private static AdjacentCoordinate AdjCoordinate { get; set; }

        /// <summary>
        ///     The length of the Map. Height is not needed
        /// </summary>
        private static int Length { get; set; }

        /// <summary>
        ///     Get and Return the complete List of Transition Tiles
        ///     If we just want to get the changed Tiles use ChangedTiles
        ///     Key Coordinate Id, Value List of Tile Ids
        /// </summary>
        internal Dictionary<int, List<int>> TransitionDictionary { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     We need a copy of the current Transitions not a Reference
        /// </summary>
        /// <returns>
        ///     A Snapshot of the current Transitions, it is ridiculous ....<see cref="T:Dictionary{int, List{int}}" />
        /// </returns>
        public Dictionary<int, List<int>> GetTransitions()
        {
            return TransitionDictionary.Clone();
        }

        /// <inheritdoc />
        /// <summary>
        ///     For speed ups in the Editor.
        ///     We note changed Tiles.
        ///     We must still watch out if the Tile is null now or removed do the Same in Cells
        ///     Returns all Transitions
        ///     Handles Multi and simple Terrain Transitions
        ///     Multi will be handled over a Linked List. Successor follows up another one, until the End of the chain is reached
        ///     Unlike basic Transitions, Multi Terrain will be stored completely in the TransitionDictionary
        /// </summary>
        /// <param name="coordinate">Target Coordinate</param>
        /// <returns>Dictionary, Key is MasterId, Value is Tile Id<see cref="T:Dictionary{int, int}" />, can return null.</returns>
        /// <exception cref="T:TransitionEngine.TransitionException"></exception>
        /// <exception cref="T:System.NullReferenceException"></exception>
        public Dictionary<int, int> AddTransition(Coordinates coordinate)
        {
            if (coordinate == null) throw new ArgumentException(TransitionEngineResources.ErrorWrongCoordinate);

            //check if we have CheckIfMultiTerrain
            if (!TransitionProcessing.CheckIfTransitionTerrain(coordinate.TileId))
            {
                var tilePlacement = BasicPreparations(coordinate);
                if (tilePlacement == null) return null;

                //Put all the Files into Place
                TransitionDictionary = TransitionProcessing.ConvergeParts(TransitionDictionary, tilePlacement, false);
                return tilePlacement;
            }

            var multiTile = AdvancedPreparations(coordinate);
            //Error since no Multi terrain Tile has only one Tile!, The One Tile is added by default

            if (multiTile == null || multiTile.Count == 1)
                throw new TransitionException(TransitionEngineResources.MultiTerrainNotFitting);

            //Check if we would overlap an existing Multi terrain, if we do Throw an Exception
            if (!TransitionProcessing.CheckMultiTerrainPlacement(multiTile, TransitionDictionary))
                throw new TransitionException(TransitionEngineResources.MultiTerrainAlreadyInplace);

            //Put all the Files into Place
            TransitionDictionary = TransitionProcessing.ConvergeParts(TransitionDictionary, multiTile, true);

            return multiTile;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Deletes all Transitions of a specific Tile
        /// </summary>
        /// <param name="coordinate">Target Coordinate</param>
        /// <returns>Dictionary, Key is MasterId, Value is Tile Id<see cref="T:Dictionary{int, int}" />.</returns>
        public Dictionary<int, int> DeleteTransition(Coordinates coordinate)
        {
            //not initialized? Return
            if (TransitionDictionary.IsNullOrEmpty()) return null;

            coordinate = CheckTileStatus(coordinate);
            if (coordinate == null) return null;

            var tile = TransitionEngineRegister.GetTile(coordinate.TileId);
            if (tile == null) return null;

            if (tile.TileType == Tile.TileTypes.MultiTerrain) return DeleteMultiTile(coordinate);

            var tileplacement = BasicPreparations(coordinate);
            if (tileplacement.IsNullOrEmpty()) return null;
            //Put all the Files into Place
            TransitionDictionary = TransitionProcessing.RemoveParts(TransitionDictionary, tileplacement);

            return tileplacement;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Serialize the Object
        /// </summary>
        /// <param name="path">Save Path</param>
        public void SaveTransition(string path)
        {
            TransitionLoader.SaveTransition(TransitionDictionary, path);
        }

        /// <summary>
        ///     Check if we have even some Transitions or Multi terrain in place.
        /// </summary>
        /// <param name="item">The Coordinate item.</param>
        /// <returns>Null or fitting Coordinate <see cref="Coordinates" />.</returns>
        private Coordinates CheckTileStatus(Coordinates item)
        {
            var id = ArtShared.CalculateId(item, Length);

            if (!TransitionDictionary.ContainsKey(id)) return null;

            foreach (var tileId in TransitionDictionary[id])
            {
                var tile = TransitionEngineRegister.GetTile(tileId);

                if (tile == null) return null;

                if (tile.TileType == Tile.TileTypes.TerrainWithTransitions && tile.IdOfMaster == 0)
                {
                    item.TileId = tileId;
                    item.ZLayer = tile.Layer;
                    return item;
                }

                if (tile.TileType != Tile.TileTypes.MultiTerrain) continue;

                item.TileId = tile.IdOfMaster;
                item.ZLayer = tile.Layer;
                return item;
            }

            return null;
        }

        /// <summary>
        ///     Complex Handling of deleting Multi Terrain Tile
        /// </summary>
        /// <param name="coordinate">Target Coordinate</param>
        /// <returns>Dictionary, Key is MasterId, Value is Tile Id</returns>
        private Dictionary<int, int> DeleteMultiTile(Coordinates coordinate)
        {
            //not initialized? Return
            if (TransitionDictionary.IsNullOrEmpty()) return null;

            //Test Input = 5
            var masterId = ArtShared.CalculateId(coordinate, Length); //Correct

            //Error
            if (!TransitionDictionary.ContainsKey(masterId))
            {
                DebugLog.CreateLogFile(string.Concat(TransitionEngineResources.ErrorWrongKey, masterId), ErCode.Error);
                return null;
            }

            //Test Input: Tile id = 3, List
            var multiTileId = TransitionEngineRegister.CheckIfMultiTerrain(TransitionDictionary[masterId]);
            //We got out 3, Tile Id == 3, Location is 5

            //only used if we have a Multi Terrain Tile in this Area
            if (multiTileId == TransitionEngineResources.ErrorNumber) return null;

            //we got Test Input 3
            var multiTile = RemoveMultiTerrainTile(multiTileId, masterId);

            if (multiTile.IsNullOrEmpty()) return null;

            //Finally delete the Crap and tell all the Stats that we moved on
            TransitionDictionary = TransitionProcessing.RemoveParts(TransitionDictionary, multiTile);
            return multiTile;
        }

        /// <summary>
        ///     Generates simple Tile with all Transitions
        ///     Duplicates won't appear anymore
        /// </summary>
        /// <param name="coordinate">Target Coordinate</param>
        /// <returns>Merged Transition Dictionary, Key is MasterId, Value is Tile Id</returns>
        private static Dictionary<int, int> BasicPreparations(Coordinates coordinate)
        {
            //here we get the Transitions and their directions
            //Key = Direction, Value =Tile Id
            var transitions = TransitionEngineRegister.GetAllTransitions(coordinate.TileId);

            //should not happen but what can a man do?
            if (transitions == null)
            {
                DebugLog.CreateLogFile(TransitionEngineResources.ErrorWrongCoordinate, ErCode.Error, coordinate);
                return null;
            }

            //Get Placements
            var masterId = ArtShared.CalculateId(coordinate, Length);
            var tilePlacement = AdjCoordinate.GenerateCoordinates(transitions, masterId);
            //add Base Tile
            tilePlacement.Add(masterId, coordinate.TileId);

            return tilePlacement;
        }

        /// <summary>
        ///     Merges new Elements for the Dictionary with the new ones at the End
        ///     Generated huge Tile with a linked List one successor one predecessor
        ///     A Multi Terrain Tile
        ///     Master Tile is included
        /// </summary>
        /// <param name="coordinate">Target Coordinate</param>
        /// <returns>Dictionary, Key is MasterId, Value is Tile Id</returns>
        private static Dictionary<int, int> AdvancedPreparations(Coordinates coordinate)
        {
            //Get Placements
            var masterId = ArtShared.CalculateId(coordinate, Length);
            //here we get the Transitions and their directions
            var tilePlacement = TransitionProcessing.GetAllMultiTransitions(coordinate.TileId);

            //Key = Direction, Value =Tile Id
            return TransitionProcessing.GetComplexTransitions(tilePlacement,
                AdjCoordinate, masterId, coordinate.TileId);
        }

        /// <summary>
        ///     Get the targeted Multi Tile, as long you click on one of the targeted Tile
        /// </summary>
        /// <param name="multiTileId">Id of Tile</param>
        /// <param name="startId">The clicked Id</param>
        /// <returns>Dictionary, Key is MasterId, Value is Tile Id</returns>
        private Dictionary<int, int> RemoveMultiTerrainTile(int multiTileId, int startId)
        {
            //First get the Master File, Test Case Tile Id == 3, result should be 0
            var masterTileId = TransitionProcessing.GetMasterTile(multiTileId);

            //we should get 0 from test, that is the Tile id, since it is the first Tile
            if (masterTileId == TransitionEngineResources.ErrorNumber)
            {
                DebugLog.CreateLogFile(TransitionEngineResources.ErrorCouldnotfindStart, ErCode.Error);
                return null;
            }

            /*
            *  Get all places of the Master Tile, Test should only return 0, input: 0, TransitionDictionary
            *  go though each TransitionDictionary
            *  check if the Point is in it take the one that has the point in it!than we can stop it is only one element in and it is 0
            */
            foreach (var masterId in TransitionProcessing.GetAllStartPoints(masterTileId, TransitionDictionary))
            {
                //input 0 (Tile Id), , and Tile Dictionary
                var tilePlacement = TransitionProcessing.GetAllMultiTransitions(masterTileId);

                //input: tile-placement, _adjCoordinate,masterId == coordinate 0
                var multiTile = TransitionProcessing.GetComplexTransitions(tilePlacement, AdjCoordinate, masterId,
                    masterTileId);

                //we should now input 5
                if (TransitionProcessing.DoesMultiTileContainTile(startId, multiTile)) return multiTile;
            }

            return null;
        }
    }
}