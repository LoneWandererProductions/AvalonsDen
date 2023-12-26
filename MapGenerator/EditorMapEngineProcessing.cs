/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/MapGenerator/EditorMapEngineProcessingTiles.cs
 * PURPOSE:     Change Tiles accordingly to Tile changes
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Text;
using Resources;

namespace MapGenerator
{
    /// <summary>
    ///     The editor map engine processing class.
    /// </summary>
    internal static class EditorMapEngineProcessing
    {
        /// <summary>
        ///     Clean ups to speed up the path-finding Algorithm more walls less overhead
        /// </summary>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <param name="borderArray">Borders as Array</param>
        /// <returns>The MapObject as <see cref="MapObject" /></returns>
        internal static List<string> GenerateBorderMap(int height, int length, int[,] borderArray)
        {
            return LoadBorderMapFromArray(borderArray, height, length);
        }

        /// <summary>
        ///     The changed border map.
        /// </summary>
        /// <param name="borderArray">Borders as Array</param>
        /// <param name="tileChanges">The Tile Changes.</param>
        /// <param name="masterTileDictionary">The masterTileDictionary.</param>
        /// <param name="borderDictionary">The borderDictionary.</param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <returns>The modified border Array.</returns>
        internal static int[,] ChangeBorderMap(int[,] borderArray, IEnumerable<Coordinates> tileChanges,
            Dictionary<int, Tile> masterTileDictionary, Dictionary<int, TileBorders> borderDictionary, int height,
            int length)
        {
            foreach (var tile in tileChanges)
            {
                if (tile.TileId <= 1) continue;

                var tileInfo = masterTileDictionary[tile.TileId];

                var borderCache = borderDictionary[tileInfo.BorderId];

                //Convert the Coordinates into a new format:
                var convertTile = GetPoint(tile);

                //Help Method changes Border Array
                borderArray = SetCleanPath(borderCache, convertTile.XRow, convertTile.YColumn, borderArray, height,
                    length);

                borderArray = SetBlockPath(borderCache, convertTile.XRow, convertTile.YColumn, borderArray);
            }

            return borderArray;
        }

        /// <summary>
        ///     Help Method changes Border Array
        ///     NW|N|NE
        ///     W |X| E
        ///     SW|S|SE
        /// </summary>
        /// <param name="borders">Borders of the target Tile</param>
        /// <param name="rowX">Target Row</param>
        /// <param name="columnY">Target Column</param>
        /// <param name="borderArray">Borders as Array</param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <returns>Modified border Array.</returns>
        private static int[,] SetCleanPath(TileBorders borders, int rowX, int columnY, int[,] borderArray, int height,
            int length)
        {
            //Base Tile
            if (!borders.BlockAble) borderArray[rowX, columnY] = MapGeneratorResources.Path;

            //Paths
            //North
            if (!borders.BorderNorth && columnY != 1 && columnY != height * MapGeneratorResources.CellHeight - 2)
                borderArray[rowX, columnY - 1] = MapGeneratorResources.Path;
            //East
            if (!borders.BorderEast && rowX != length * MapGeneratorResources.CellHeight - 2)
                borderArray[rowX + 1, columnY] = MapGeneratorResources.Path;
            //South
            if (!borders.BorderSouth && columnY != height * MapGeneratorResources.CellHeight - 2)
                borderArray[rowX, columnY + 1] = MapGeneratorResources.Path;
            //West
            if (!borders.BorderWest && rowX != 1 && columnY != height * MapGeneratorResources.CellHeight - 2)
                borderArray[rowX - 1, columnY] = MapGeneratorResources.Path;

            return borderArray;
        }

        /// <summary>
        ///     Help Method changes Border Array
        ///     NW|N|NE
        ///     W |X| E
        ///     SW|S|SE
        /// </summary>
        /// <param name="borders">Borders of the target Tile</param>
        /// <param name="rowX">Target Row</param>
        /// <param name="columnY">Target Column</param>
        /// <param name="borderArray">Borders as Array</param>
        /// <returns>Modified border Array.</returns>
        private static int[,] SetBlockPath(TileBorders borders, int rowX, int columnY, int[,] borderArray)
        {
            //Base Tile
            if (borders.BlockAble) borderArray[rowX, columnY] = MapGeneratorResources.Wall;

            //Paths
            //North
            if (borders.BorderNorth) borderArray[rowX, columnY - 1] = MapGeneratorResources.Wall;

            //East
            if (borders.BorderEast) borderArray[rowX + 1, columnY] = MapGeneratorResources.Wall;

            //South
            if (borders.BorderSouth) borderArray[rowX, columnY + 1] = MapGeneratorResources.Wall;

            //West
            if (borders.BorderWest) borderArray[rowX - 1, columnY] = MapGeneratorResources.Wall;

            return borderArray;
        }

        /// <summary>
        ///     Helper Method returns BorderMap as List
        /// </summary>
        /// <param name="borderArray">Borders as Array</param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <returns>Returns changed Map Layer as list as <see cref="T:List{string}" />.</returns>
        internal static List<string> LoadBorderMapFromArray(int[,] borderArray, int height, int length)
        {
            //Optimization: reserves this amount of Space of Memory: _mapHeight *3
            var borders = new List<string>(height * MapGeneratorResources.CellHeight);
            //Create Border Map base
            for (var y = 0; y < height * MapGeneratorResources.CellHeight; y++) // Map.IntMapLengthX
            {
                var sb = new StringBuilder();

                for (var x = 0; x < length * MapGeneratorResources.CellHeight; x++)
                {
                    var z = borderArray[x, y];
                    if (x == length * MapGeneratorResources.CellHeight - 1)
                    {
                        sb.Append(z);
                    }
                    else
                    {
                        sb.Append(z);
                        sb.Append(MapGeneratorResources.Separator);
                    }
                }

                borders.Add(sb.ToString());
            }

            return borders;
        }

        /// <summary>
        ///     Transforms Nodes to Coordinates
        /// </summary>
        /// <param name="newPoint">Converted new Point</param>
        /// <remarks>Necessary since One Cell has a height and length of 3 but is handled as  1</remarks>
        /// <returns>Nodes to  Coordinates <see cref="Coordinates" />.</returns>
        private static Coordinates GetPoint(Coordinates newPoint)
        {
            var point = new Coordinates();
            var playerYColumn = 1;
            if (newPoint.YColumn != 0) playerYColumn = newPoint.YColumn * 3 + 1;

            var playerXRow = 1;
            if (newPoint.XRow != 0) playerXRow = newPoint.XRow * 3 + 1;

            point.XRow = playerXRow;
            point.YColumn = playerYColumn;
            return point;
        }
    }
}