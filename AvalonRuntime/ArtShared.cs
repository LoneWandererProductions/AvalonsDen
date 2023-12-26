/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/Cells.cs
 * PURPOSE:     Helper class that handles the creation of the Dictionary
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

namespace AvalonRuntime
{
    /// <summary>
    ///     Helper for the Tile Registry
    /// </summary>
    public static class ArtShared
    {
        /// <summary>
        ///     Example:
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        /// </summary>
        /// <param name="coordinate">Point on the Map</param>
        /// <param name="length">length of the Map</param>
        /// <returns>Fitting id of the Coordinate</returns>
        public static int CalculateId(Coordinates coordinate, int length)
        {
            return coordinate.YColumn * length + coordinate.XRow;
        }

        /// <summary>
        ///     Extension of IdToCoordinate,
        ///     Adds Layer and Id
        /// </summary>
        /// <param name="masterId">Point on the Map</param>
        /// <param name="length">length of the Map</param>
        /// <param name="layer">Layer of the Map</param>
        /// <param name="tileId">Id of Tile</param>
        /// <returns>Id from Coordinate</returns>
        public static Coordinates IdToCoordinate(int masterId, int length, int layer, int tileId)
        {
            var coordinate = IdToCoordinate(masterId, length);
            coordinate.TileId = tileId;
            coordinate.ZLayer = layer;
            return coordinate;
        }

        /// <summary>
        ///     Extension of IdToCoordinate,
        ///     Adds Layer and Id
        /// </summary>
        /// <param name="masterId">Point on the Map</param>
        /// <param name="length">length of the Map</param>
        /// <param name="layer">Layer of the Map</param>
        /// <returns>Coordinate from Id</returns>
        public static Coordinates IdToCoordinate(int masterId, int length, int layer)
        {
            var coordinate = IdToCoordinate(masterId, length);
            coordinate.ZLayer = layer;
            return coordinate;
        }

        /// <summary>
        ///     Example:
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        /// </summary>
        /// <param name="masterId">Point on the Map</param>
        /// <param name="length">length of the Map</param>
        /// <returns>Fitting Coordinate of the id</returns>
        public static Coordinates IdToCoordinate(int masterId, int length)
        {
            var modulo = masterId % length;
            var yColumn = masterId / length;

            return new Coordinates
            {
                XRow = modulo,
                YColumn = yColumn
            };
        }
    }
}