/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/ResourcesTransitions.cs
 * PURPOSE:     Basic Resources for Avalon Transition Tests
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The resources transitions class.
    /// </summary>
    internal static class ResourcesTransitions
    {
        /// <summary>
        ///     Generates Surrounding Struct Points
        ///     -1,1  0,1   1,1   NW,N,NE   812     -1,-1   0,-1    1,-1
        ///     -1,0  0,0   1,0    S,0,E    7x3     -1,0    0,0     1,0
        ///     -1,-1 0,-1  1,-1   SW,W,SE  654     -1,1    0,1     1,1
        /// </summary>
        internal static readonly Tile TileMaster = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0
        };

        /// <summary>
        ///     The tile n (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 1 }.
        /// </summary>
        internal static readonly Tile TileN = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 1
        };

        /// <summary>
        ///     The tile ne (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 2 }.
        /// </summary>
        internal static readonly Tile TileNe = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 2
        };

        /// <summary>
        ///     The tile e (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 3 }.
        /// </summary>
        internal static readonly Tile TileE = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 3
        };

        /// <summary>
        ///     The tile se (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 4 }.
        /// </summary>
        internal static readonly Tile TileSe = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 4
        };

        /// <summary>
        ///     The tile w (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 5 }.
        /// </summary>
        internal static readonly Tile TileW = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 5
        };

        /// <summary>
        ///     The tile sw (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 6 }.
        /// </summary>
        internal static readonly Tile TileSw = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 6
        };

        /// <summary>
        ///     The tile s (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 7 }.
        /// </summary>
        internal static readonly Tile TileS = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 7
        };

        /// <summary>
        ///     The tile nw (readonly). Value: new Tile { TileType = Tile.TileType.TerrainwithTransitions, IdofMaster = 0,
        ///     DirectionOfTransition = 8 }.
        /// </summary>
        internal static readonly Tile TileNw = new()
        {
            TileType = Tile.TileTypes.TerrainWithTransitions,
            IdOfMaster = 0,
            DirectionOfTransition = 8
        };

        /// <summary>
        ///     Multi Terrain: First Example
        ///     xxx
        ///     012
        ///     x3x
        ///     Generates Surrounding Struct Points
        ///     -1,1  0,1   1,1   NW,N,NE   812     -1,-1   0,-1    1,-1
        ///     -1,0  0,0   1,0    S,0,E    7x3     -1,0    0,0     1,0
        ///     -1,-1 0,-1  1,-1   SW,W,SE  654     -1,1    0,1     1,1
        ///     Id: 0
        /// </summary>
        internal static readonly Tile TileMasterMt = new()
        {
            TileType = Tile.TileTypes.MultiTerrain,
            IdOfMaster = 0
        };

        /// <summary>
        ///     Id: 1
        /// </summary>
        internal static readonly Tile TileNmt1 = new()
        {
            TileType = Tile.TileTypes.MultiTerrain,
            IdOfMaster = 0,
            DirectionOfTransition = 3
        };

        /// <summary>
        ///     Id: 2
        /// </summary>
        internal static readonly Tile TileNmt2 = new()
        {
            TileType = Tile.TileTypes.MultiTerrain,
            IdOfMaster = 1,
            DirectionOfTransition = 3
        };

        /// <summary>
        ///     Id: 3
        /// </summary>
        internal static readonly Tile TileEmt = new()
        {
            TileType = Tile.TileTypes.MultiTerrain,
            IdOfMaster = 2,
            DirectionOfTransition = 6
        };
    }
}