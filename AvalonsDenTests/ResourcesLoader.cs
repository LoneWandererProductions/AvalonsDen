/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/ResourcesLoader.cs
 * PURPOSE:     Resources for basic objects needed
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The resources loader class.
    /// </summary>
    internal static class ResourcesLoader
    {
        /// <summary>
        ///     TODO REMOVE internals like Void Border and Void, flag
        ///     TODO add to regeneration
        ///     TODO Missing Tiles!
        /// </summary>
        internal static readonly Dictionary<int, Tile> MasterTile = new()
        {
            {
                0,
                new Tile
                {
                    FileName = "Void.png",
                    Layer = 1,
                    TileType = Tile.TileTypes.InternalTiles,
                    BorderId = 0,
                    IdOfMaster = 1,
                    DirectionOfTransition = 0
                }
            },
            {
                1,
                new Tile
                {
                    FileName = "border.png",
                    Layer = 6,
                    TileType = Tile.TileTypes.InternalTiles,
                    BorderId = 0,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                2,
                new Tile
                {
                    FileName = "Void.png",
                    Layer = 1,
                    TileType = Tile.TileTypes.InternalTiles,
                    BorderId = 10,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                3,
                new Tile
                {
                    FileName = "Dot.png",
                    Layer = 6,
                    TileType = Tile.TileTypes.InternalTiles,
                    BorderId = 0,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                4,
                new Tile
                {
                    FileName = "flag.png",
                    Layer = 8,
                    TileType = Tile.TileTypes.InternalTiles,
                    BorderId = 0,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                5,
                new Tile
                {
                    FileName = "grass.png",
                    Layer = 1,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 10,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                6,
                new Tile
                {
                    FileName = "tile_hile_1_e.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 2,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                7,
                new Tile
                {
                    FileName = "tile_hile_1_w.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 3,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                8,
                new Tile
                {
                    FileName = "tile_hile_1_s.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 4,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                9,
                new Tile
                {
                    FileName = "tile_hile_1_n.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 5,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                10,
                new Tile
                {
                    FileName = "tile_hile_1_ne.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 6,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                11,
                new Tile
                {
                    FileName = "tile_hile_1_nw.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 7,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                12,
                new Tile
                {
                    FileName = "tile_hile_1_se.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 8,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                13,
                new Tile
                {
                    FileName = "tile_hile_1_sw.png",
                    Layer = 2,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 9,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                14,
                new Tile
                {
                    FileName = "drakecoloredwv5.png",
                    Layer = 5,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 0,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                15,
                new Tile
                {
                    FileName = "hill.png",
                    Layer = 1,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 10,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                16,
                new Tile
                {
                    FileName = "Flieder.png",
                    Layer = 4,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 10,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                17,
                new Tile
                {
                    FileName = "Morganwv1.png",
                    Layer = 5,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 1,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                18,
                new Tile
                {
                    FileName = "Portal.png",
                    Layer = 4,
                    TileType = Tile.TileTypes.NoTransitions,
                    BorderId = 1,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                19,
                new Tile
                {
                    FileName = "path.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 0,
                    DirectionOfTransition = 0
                }
            },
            {
                20,
                new Tile
                {
                    FileName = "path_n.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 1
                }
            },
            {
                21,
                new Tile
                {
                    FileName = "path_s.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 5
                }
            },
            {
                22,
                new Tile
                {
                    FileName = "path_e.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 3
                }
            },
            {
                23,
                new Tile
                {
                    FileName = "path_w.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 7
                }
            },
            {
                24,
                new Tile
                {
                    FileName = "path_ne.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 2
                }
            },
            {
                25,
                new Tile
                {
                    FileName = "path_se.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 4
                }
            },
            {
                26,
                new Tile
                {
                    FileName = "path_sw.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 6
                }
            },
            {
                27,
                new Tile
                {
                    FileName = "path_nw.png",
                    Layer = 3,
                    TileType = Tile.TileTypes.TerrainWithTransitions,
                    BorderId = 0,
                    IdOfMaster = 19,
                    DirectionOfTransition = 8
                }
            }
        };

        /// <summary>
        ///     The master border (readonly). Value: new Dictionary&lt;int, TileBorders&gt;
        /// </summary>
        internal static readonly Dictionary<int, TileBorders> MasterBorder = new()
        {
            {
                0,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                1,
                new TileBorders
                {
                    BlockAble = true,
                    BorderNorth = true,
                    BorderEast = true,
                    BorderSouth = true,
                    BorderWest = true,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                2,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = true,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                3,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = true,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                4,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = true,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                5,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = true,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                6,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = true,
                    BorderEast = true,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                7,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = true,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = true,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                8,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = true,
                    BorderSouth = true,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                9,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = true,
                    BorderWest = true,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                10,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            },
            {
                11,
                new TileBorders
                {
                    BlockAble = false,
                    BorderNorth = false,
                    BorderEast = false,
                    BorderSouth = false,
                    BorderWest = false,
                    BorderNorthWest = false,
                    BorderNorthEast = false,
                    BorderSouthEast = false,
                    BorderSouthWest = false
                }
            }
        };
    }
}